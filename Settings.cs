using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace SomeXamarinFormsApp.Services
{
    public class Settings : ObservableSettings
    {
        public static Settings Default { get; } = new Settings();

        [DefaultSettingValue(Value = 0)]
        public int SomeNumber { get => Get<int>(); set => Set(value); }

        [DefaultSettingValue(Value = "")]
        public string SomeString { get => Get<string>(); set => Set(value); }

        [DefaultSettingValue(Value = DateTime.Today)]
        public DateTime SomeDateTime { get => Get<DateTime>(); set => Set(value); }
    }

    public class ObservableSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            bool containsKey = Preferences.ContainsKey(propertyName);
            switch (value)
            {
                case int val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case string val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case double val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case bool val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case DateTime val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case float val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
                case long val:
                    if (containsKey && EqualityComparer<T>.Default.Equals((T)(object)Preferences.Get(propertyName, val), value))
                        return false;
                    Preferences.Set(propertyName, val);
                    break;
            }
            OnPropertyChanged(propertyName);
            return true;
        }

        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (Preferences.ContainsKey(propertyName))
                switch (default(T))
                {
                    case int val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case string val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case double val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case bool val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case DateTime val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case float val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                    case long val:
                        return (T)(Preferences.Get(propertyName, val) as object);
                }

            var attributes = GetType().GetTypeInfo().GetDeclaredProperty(propertyName).CustomAttributes.Where(ca => ca.AttributeType == typeof(DefaultSettingValueAttribute)).ToList();
            if (attributes.Count == 1)
                return (T)attributes[0].NamedArguments[0].TypedValue.Value;

            return default(T);
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DefaultSettingValueAttribute : Attribute
    {
        public DefaultSettingValueAttribute()
        {
        }

        public DefaultSettingValueAttribute(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
