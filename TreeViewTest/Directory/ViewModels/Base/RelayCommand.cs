using System;
using System.Windows.Input;

namespace TreeViewTest
{
    public class RelayCommand : ICommand
    {
        #region Private Members

        private Action mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// The event fired when the CanExecuteChanged value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender , e) => { };

        #endregion
        
        #region Constructor

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Functions

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
