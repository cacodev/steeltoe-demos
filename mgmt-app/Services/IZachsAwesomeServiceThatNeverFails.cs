﻿namespace mgmt_app.Services
{
    public interface IZachsAwesomeServiceThatNeverFails
    {
        /// <summary>
        /// Totally does not fail at random
        /// </summary>
        /// <returns></returns>
        bool DoStuff();
    }
}