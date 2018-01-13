using System;
using System.Drawing;
using System.Windows.Forms;
using CaH.Shared.Kgf;
using CaH.Shared.Poco;
using CardSelector.CardProviders;

namespace CardSelector
{
    public partial class FrmCardSelector : Form
    {
        private LocalCardProvider _cardProvider;
        private Timer _flushTimer;

        public FrmCardSelector()
        {
            InitializeComponent();
            _cardProvider = new LocalCardProvider();
            Logic();
        }

        private async void Logic()
        {
            await _cardProvider.Initialize();
            ShowCard();

            _flushTimer = new Timer { Enabled = true, Interval = 3000 };
            _flushTimer.Tick += (_, __) => _cardProvider.Flush();
        }

        private void butMarkBlack_Click(object sender, EventArgs e)
        {
            OverrideCard(OverrideCardType.CallCard);
        }

        private void butMarkRed_Click(object sender, EventArgs e)
        {
            OverrideCard(OverrideCardType.Object);
        }

        private void butMarkYellow_Click(object sender, EventArgs e)
        {
            OverrideCard(OverrideCardType.Action);
        }

        private void butPick_Click(object sender, EventArgs e)
        {
            if (chkPickAll.Checked)
            {
                _cardProvider.RemoveCard();
                ShowCard();
            }
            else
            {
                _cardProvider.Current.Picked = !_cardProvider.Current.Picked;
                _cardProvider.MarkFlush();

                ShowCard();
            }
        }

        private void butPrev_Click(object sender, EventArgs e)
        {
            if (_cardProvider.MovePrevious())
            {
                ShowCard();
            }
            else
            {
                MessageBox.Show("Begin of Database");
            }
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            if (chkPickAll.Checked)
            {
                _cardProvider.Current.Picked = true;
                _cardProvider.MarkFlush();
            }

            if (_cardProvider.MoveNext())
            {
                ShowCard();
            }
            else
            {
                MessageBox.Show("End of Database");
            }
        }

        private void OverrideCard(OverrideCardType cardType)
        {
            _cardProvider.Current.CardOverride = cardType;
            _cardProvider.Current.Picked = true;
            _cardProvider.MarkFlush();
            ShowCard();
        }

        private void ShowCard()
        {
            var cCard = _cardProvider.Current;
            txtCard.Text = string.Join("_", cCard.Text);
            grpCardContainer.Text = $@"Card {(cCard.Picked ? "[Picked]" : "")}";

            if (cCard.CardOverride == OverrideCardType.Default)
            {
                if (cCard is CallCard)
                {
                    ChangeTxtComp(Color.White, Color.Black);
                }
                else if (cCard is ResponseCard)
                {
                    ChangeTxtComp(Color.White, Color.DarkRed);
                }
            }
            else
            {
                switch (cCard.CardOverride)
                {
                    case OverrideCardType.CallCard:
                        ChangeTxtComp(Color.White, Color.Black);
                        break;

                    case OverrideCardType.Object:
                        ChangeTxtComp(Color.White, Color.DarkRed);
                        break;

                    case OverrideCardType.Action:
                        ChangeTxtComp(Color.White, Color.DarkOrange);
                        break;
                }
            }
        }

        private void ChangeTxtComp(Color fore, Color back)
        {
            txtCard.ForeColor = fore;
            txtCard.BackColor = back;
        }

        private void FrmCardSelector_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    butMarkBlack.PerformClick();
                    break;

                case Keys.W:
                    butMarkRed.PerformClick();
                    break;

                case Keys.E:
                    butMarkYellow.PerformClick();
                    break;

                case Keys.S:
                    butPick.PerformClick();
                    break;

                case Keys.A:
                    butPrev.PerformClick();
                    break;

                case Keys.D:
                    butNext.PerformClick();
                    break;
            }
        }

        private void saveDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cardProvider.Flush();
            MessageBox.Show("Database Saved");
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                Filter = @"CaH Database|*.tsv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                new Reformatter(_cardProvider.Database) { PicksOnly = true }.ToFile(sfd.FileName);
                MessageBox.Show(@"Successfully exported!");
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doReset = MessageBox.Show(@"Do you want to reset the Overrides aswell?", "", MessageBoxButtons.YesNoCancel);
            if (doReset == DialogResult.Yes || doReset == DialogResult.No)
            {
                _cardProvider.Reset(doReset == DialogResult.Yes);
                MessageBox.Show(@"Successfully reseted");
                ShowCard();
            }
        }

        private void FrmCardSelector_Load(object sender, EventArgs e)
        {
        }

        private void chkPickAll_CheckedChanged(object sender, EventArgs e)
        {
            butPick.Text = (chkPickAll.Checked ? "Delete" : "Pick") + " Card";
            this.ActiveControl = txtCard;
        }
    }
}