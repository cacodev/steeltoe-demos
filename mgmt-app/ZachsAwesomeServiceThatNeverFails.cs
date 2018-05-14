using System;

namespace mgmt_app
{
    public class ZachsAwesomeServiceThatNeverFails : IZachsAwesomeServiceThatNeverFails
    {
        public bool DoStuff()
        {
            var rng = new Random(DateTime.Now.Millisecond);
            if(rng.Next(0,2) == 0)
                throw new Exception("Wait...This never happens I swear!");

            return true;
        }
    }
}