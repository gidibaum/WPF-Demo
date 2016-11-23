
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Base
{
    public class ValidationObject : ObservableObject, INotifyDataErrorInfo
    {
        public ValidationObject()
        {
            this.PropertyChanged += (s, e) => Validate(e.PropertyName);

        }

        #region INotifyDataErrorInfo

        readonly Dictionary<string, List<string>> _Errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propName)
        {
            if (_Errors.ContainsKey(propName))
                return _Errors[propName];

            return Enumerable.Empty<string>();
        }


        #region Property: HasErrors

        bool _HasErrors;

        public bool HasErrors
        {
            get { return _HasErrors; }
            set { SetProperty(ref _HasErrors, value); }
        }

        #endregion

        #region Property: IsValid

        bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set { SetProperty(ref _IsValid, value); }
        }

        #endregion

          

        public void AddError(string propertyName, IEnumerable<string> messages)
        {
            _Errors[propertyName] = messages.ToList();

            HasErrors = _Errors.Count > 0;
            IsValid = !HasErrors;

            NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (_Errors.ContainsKey(propertyName))
                _Errors.Remove(propertyName);

            HasErrors = _Errors.Count > 0;
            IsValid = !HasErrors;

            NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

        #region PropertyValidators

        Dictionary<string, Func<IEnumerable<string>>> _PropertyValidators;
        Dictionary<string, Func<IEnumerable<string>>> PropertyValidators
        {
            get
            {
                if (_PropertyValidators == null)
                    CreateValidators();

                return _PropertyValidators;
            }
        }

        void CreateValidators()
        {
            _PropertyValidators = new Dictionary<string, Func<IEnumerable<string>>>();

            foreach (var prop in this.GetType().GetProperties())
            {
                var validationAttrs = (ValidationAttribute[])
                    prop.GetCustomAttributes(typeof(ValidationAttribute), true);

                if (validationAttrs.IsNotEmpty())
                {
                    _PropertyValidators.Add(prop.Name, () =>
                    {
                        var errors = new List<ValidationResult>();

                        if (Validator.TryValidateProperty(
                              prop.GetValue(this),
                              new ValidationContext(this) { MemberName = prop.Name },
                              errors))
                        {
                            return Enumerable.Empty<string>();
                        }
                            
                        return errors.Select(e => e.ErrorMessage);
                    });
                }
            }
        }
        #endregion

        public void Validate(string propName)
        {
            if (propName.IsEmpty())
            {
                foreach (var item in PropertyValidators.Keys)
                {
                    Validate(item);
                }
            }
            else
            {
                if (PropertyValidators.ContainsKey(propName))
                {
                    var errors = PropertyValidators[propName]().ToList();

                    if (errors.IsEmpty())
                    {
                        RemoveError(propName);
                    }
                    else
                    {
                        AddError(propName, errors);
                    }
                }
            }
        }
    }
}
