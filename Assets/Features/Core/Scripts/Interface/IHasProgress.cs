using System;
using UnityEngine;

namespace Features.Core.Scripts.Interface
{
    public interface IHasProgress
    {
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;

        public class ProgressChangedEventArgs : EventArgs
        {
            public float ProgressAmount;
        }
    }
}