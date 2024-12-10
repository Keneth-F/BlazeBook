using BlazeBook.Data;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazeBook.Shared
{
    public partial class ContactForm: ComponentBase
    {
        [Parameter] public RenderFragment Fields { get; set; }
        [Parameter] public EventCallback<Contact> OnSubmit { get; set; }

        [Parameter] public Contact ContactModel { get; set; }
        private List<string> ValidationErrors { get; set; } = new();
        private bool IsSubmitting { get; set; }
        //TODO: check onParameterSet
        private async Task HandleSubmit()
        {
            ValidationErrors.Clear();

            var validationContext = new ValidationContext(ContactModel);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(ContactModel, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    ValidationErrors.Add(result.ErrorMessage);
                }
                return;
            }

            IsSubmitting = true;

            try
            {
                await OnSubmit.InvokeAsync(ContactModel);
            }
            catch (Exception ex)
            {
                ValidationErrors.Add($"Submission error: {ex.Message}");
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
