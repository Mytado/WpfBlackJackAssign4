using System;
using System.Windows.Media.Imaging;

namespace DemoCardDLL
{
    public class Card
    {

        public int Value { get; private set; }

        public enum Suites
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        private BitmapImage FrontImage;
        private BitmapImage BackImage;

        public string SuiteString { get; private set; }

        public string ValueString { get; private set; }

        public Suites Suite { get; private set; }

        public bool FaceUp { get; private set; }

        public Card(int _value, Suites _suite)
        {
            Value = _value;
            Suite = _suite;
            FaceUp = false;

            String st = Value.ToString();

            switch (Suite)
            {
                case Suites.Hearts: st += "H"; break;
                case Suites.Clubs: st += "C"; break;
                case Suites.Diamonds: st += "D"; break;
                case Suites.Spades: st += "S"; break;
            }


            SetImages(st);


            SuiteString = Suite.ToString();
            ValueString = Value.ToString();
        }

        private void SetImages(String name)
        {
            FrontImage = new BitmapImage();
            BackImage = new BitmapImage();


            BitmapImage frontBitMapImage = new BitmapImage();
            frontBitMapImage.BeginInit();

            frontBitMapImage.UriSource = new Uri($"/Cards/" + name + ".png", UriKind.Relative);
            FrontImage = frontBitMapImage;
            frontBitMapImage.EndInit();

            BitmapImage backBitMapImage = new BitmapImage();
            backBitMapImage.BeginInit();

            backBitMapImage.UriSource = new Uri($"/Cards/purple_back.png", UriKind.Relative);
            BackImage = backBitMapImage;

            backBitMapImage.EndInit();

        }

        public BitmapImage GetImage()
        {
            if (FaceUp) return FrontImage;
            else return BackImage;
        }

        public void Flip()
        {
            FaceUp = true;
        }

        public override string ToString()
        {
            return ValueString + " of " + SuiteString;
        }
    }
}
