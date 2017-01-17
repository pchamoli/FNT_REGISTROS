using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class ModelExtensions
{
    public static bool HasValue(this string value)
    {
        return !string.IsNullOrEmpty(value) && value.Trim().Length > 0;
    }

    public static void Merge(this ModelStateDictionary modelState, string[] includePropertiesValidation, params IDictionary<string, string>[] dictionaries)
    {
        Guard.AgainstNullParameter(modelState, "modelState");
        Guard.AgainstNullParameter(dictionaries, "dictionaries");
        
        foreach (string property in includePropertiesValidation)
            foreach (var modelValue in modelState.Where(x => x.Key != property))
                modelValue.Value.Errors.Clear();

        foreach (var dictionary in dictionaries)
            foreach (var item in dictionary)
                modelState.AddModelError(item.Key, item.Value);
    }

    public static void Merge(this ModelStateDictionary modelState, params IDictionary<string, string>[] dictionaries)
    {
        Guard.AgainstNullParameter(modelState, "modelState");
        Guard.AgainstNullParameter(dictionaries, "dictionaries");

        string[] includePropertiesValidation = new string[] { "" };

        Merge(modelState, includePropertiesValidation, dictionaries);
    }

    public static Dictionary<string, string> GetErrorsFromModelState(this ModelStateDictionary modelState)
    {
        var errors = new Dictionary<string, string>();

        foreach (string key in modelState.Keys)
        {
            if (modelState[key].Errors.Count > 0)
            {
                errors.Add(key, modelState[key].Errors[0].ErrorMessage);
            }
        }

        return errors;
    }

}

public static class Guard
{
    public static void AgainstNullParameter(object parameter, string parameterName)
    {
        if (parameter == null) throw new ArgumentNullException(parameterName);
    }
}