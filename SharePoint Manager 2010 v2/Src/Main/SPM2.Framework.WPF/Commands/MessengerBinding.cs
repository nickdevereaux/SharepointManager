using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Windows.Controls;


namespace SPM2.Framework.WPF
{
    public class ExecuteMessage
    {
        public ExecutedRoutedEventArgs Parameter { get; private set; }

        public ExecuteMessage(ExecutedRoutedEventArgs parameter)
        {
            Parameter = parameter;
        }

        public static void Register(object recipient, object target, Action<ExecuteMessage> action)
        {
            Messenger.Default.Register<ExecuteMessage>(
                recipient,
                target,
                action);
        }

        //public static void Register(UIElement recipient, Action<ExecuteMessage> action)
        //{
        //    var win = recipient.FindAncestor<Window>();
        //    ExecuteMessage.Register(recipient, win, action);
        //}

    }
    
    public class CanExecuteMessage
    {
        Action<bool> _callback = null;

        public CanExecuteRoutedEventArgs Parameter { get; private set; }

        public CanExecuteMessage(CanExecuteRoutedEventArgs parameter, Action<bool> callback)
        {
            Parameter = parameter;
            _callback = callback;
        }

        public void CanExecute(bool parameter)
        {
            if (_callback != null)
            {
                _callback.Invoke(parameter);
            }
        }

        public static void Register(object recipient, object target, Action<CanExecuteMessage> action)
        {
            Messenger.Default.Register<CanExecuteMessage>(
                recipient,
                target,
                action);
        }
    }

    public static class MessengerBinding
    {
        public static RoutedCommand Bind(UIElement target, RoutedCommand command)
        {
            target.CommandBindings.AddCommandExecutedHandler(command,  Execute );
            target.CommandBindings.AddCommandCanExecuteHandler(command, CanExecute);

            return command;
        }

        private static void Execute(object sender, ExecutedRoutedEventArgs parameter)
        {
            Messenger.Default.Send<ExecuteMessage>(new ExecuteMessage(parameter), parameter.Command);
            parameter.Handled = true;
        }

        [DebuggerStepThrough]
        private static void CanExecute(object sender, CanExecuteRoutedEventArgs parameter)
        {
            var message = new CanExecuteMessage(
                parameter,
                callbackMessage =>
                {
                    // This is the callback code
                    if (callbackMessage)
                    {
                        parameter.CanExecute = true;
                    }
                });

            Messenger.Default.Send<CanExecuteMessage>(message, parameter.Command);
        }

    }
}
