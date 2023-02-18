using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using DataObjects;
using LogicLayer;


namespace PresentationLayer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private ResourceManager _rm = new ResourceManager(
            "PresentationLayer.CardResources", Assembly.GetExecutingAssembly());
        private int _playerBank = 500;
        private Dealer _dealer = new Dealer();
        private Banker _banker = new Banker();
        private Hand _hand;
        
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            newGame();

            // test code
            // testCardDeck();
        }

        private void newGame()
        {
            _playerBank = 500;
            resetCardDisplay();
        }

        private void testCardDeck() // test code
        {
            Deck deck = new Deck();
            deck.Shuffle();

            Card card = deck.NextCard();
            picCard1.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard1.Text = card.ToString();

            card = deck.NextCard();
            picCard2.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard2.Text = card.ToString();

            card = deck.NextCard();
            picCard3.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard3.Text = card.ToString();

            card = deck.NextCard();
            picCard4.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard4.Text = card.ToString();

            card = deck.NextCard();
            picCard5.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard5.Text = card.ToString();
        }

        private void resetCardDisplay()
        {
            // reset card images to show card backs, clear the labels, reset checkboxes
            picCard1.Image = (Image)_rm.GetObject("CardBack");
            picCard2.Image = (Image)_rm.GetObject("CardBack");
            picCard3.Image = (Image)_rm.GetObject("CardBack");
            picCard4.Image = (Image)_rm.GetObject("CardBack");
            picCard5.Image = (Image)_rm.GetObject("CardBack");

            lblCard1.Text = "";
            lblCard2.Text = "";
            lblCard3.Text = "";
            lblCard4.Text = "";
            lblCard5.Text = "";

            chkCard1.Checked = false;
            chkCard2.Checked = false;
            chkCard3.Checked = false;
            chkCard4.Checked = false;
            chkCard5.Checked = false;

            numBet.Enabled = true;
            numBet.Maximum = (decimal)_playerBank;
            numBet.Value = 1;

            btnDealNewHand.Enabled = true;
            btnDrawCards.Enabled = false;
            btnScoreHand.Enabled = false;
            lblBankAmount.Text = _playerBank.ToString("c");
            statLabel.Text = "Place a bet and press Deal New Hand to begin.";
        }

        private void btnDealNewHand_Click(object sender, EventArgs e)
        {
            _hand = _dealer.NewHand();
            displayHand();

            // move the app to the DrawCards state
            numBet.Enabled = false;
            btnDealNewHand.Enabled = false;
            btnDrawCards.Enabled = true;

            _playerBank -= (int)numBet.Value;
            lblBankAmount.Text = _playerBank.ToString("c");
            statLabel.Text = "Choose which cards to keep and press Draw Cards.";
        }

        private void displayHand()
        {
            Card card = _hand.Cards[0];
            picCard1.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard1.Text = card.ToString();

            card = _hand.Cards[1];
            picCard2.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard2.Text = card.ToString();

            card = _hand.Cards[2];
            picCard3.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard3.Text = card.ToString();

            card = _hand.Cards[3];
            picCard4.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard4.Text = card.ToString();

            card = _hand.Cards[4];
            picCard5.Image = (Image)_rm.GetObject(card.ImageName);
            lblCard5.Text = card.ToString();
        }

        private void picCard1_Click(object sender, EventArgs e)
        {
            chkCard1.Checked = !chkCard1.Checked;
        }

        private void picCard2_Click(object sender, EventArgs e)
        {
            chkCard2.Checked = !chkCard2.Checked;
        }

        private void picCard3_Click(object sender, EventArgs e)
        {
            chkCard3.Checked = !chkCard3.Checked;
        }

        private void picCard4_Click(object sender, EventArgs e)
        {
            chkCard4.Checked = !chkCard4.Checked;
        }

        private void picCard5_Click(object sender, EventArgs e)
        {
            chkCard5.Checked = !chkCard5.Checked;
        }

        private void btnDrawCards_Click(object sender, EventArgs e)
        {
            // we need to capture the state of the checkboxes
            _hand.Keep[0] = chkCard1.Checked;
            _hand.Keep[1] = chkCard2.Checked;
            _hand.Keep[2] = chkCard3.Checked;
            _hand.Keep[3] = chkCard4.Checked;
            _hand.Keep[4] = chkCard5.Checked;

            // get replacement cards from the dealer
            _dealer.DrawCards(_hand);

            displayHand();

            btnDrawCards.Enabled = false;
            btnScoreHand.Enabled = true;
            statLabel.Text = "Press Score Hand to find out how you did.";
        }

        private void btnScoreHand_Click(object sender, EventArgs e)
        {
            // _hand.sortHand();
            // displayHand();
            HandValue handValue = _hand.ScoreHand();
            int score = (int)handValue;
            string hand = Enum.GetName(typeof(HandValue), handValue).Replace('_', ' ');

            MessageBox.Show("Your hand is: " + hand + ".\n\n"
                            + "That pays " + score.ToString() + " to 1.", "Results:");

            // adjust the player's bank by multiplying bet times score
            _playerBank += (int)numBet.Value * score;

            // did the player lose?
            if (_playerBank == 0)
            {
                MessageBox.Show("You lose. Click 'Okay' to restart.", "Busted!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                newGame();
            }
            else
            {
                // return to the starting state
                resetCardDisplay();
            }
        }

        private void mnuNewGame_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Really quit?", "Restart Game",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                newGame();
            }
            return;
        }

        private void mnuSaveGame_Click(object sender, EventArgs e)
        {
            try
            {
                if (_banker.SavePlayerBank(_playerBank))
                {
                    MessageBox.Show("Progress saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void mnuRestoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                _playerBank = _banker.RestoreSavedBank();
                {
                    MessageBox.Show("Game restored.");
                }
                resetCardDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
