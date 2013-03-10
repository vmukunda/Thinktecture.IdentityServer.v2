﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.IdentityServer.Models
{
    public class Client : IValidatableObject
    {
        [UIHint("HiddenInput")]
        public int ID { get; set; }

        [Display(Name = "Name", Description = "Display name.")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description", Description = "Description.")]
        [Required]
        public string Description { get; set; }
        
        [Display(Name = "Client ID", Description = "Client ID.")]
        [Required]
        public string ClientId { get; set; }

        [Display(Name = "Client Secret", Description = "Client secret.")]
        [UIHint("SymmetricKey")]
        public string ClientSecret { get; set; }

        [UIHint("HiddenInput")]
        public bool HasClientSecret { get; set; }

        [AbsoluteUri]
        [Display(Name = "Redirect URI", Description = "Redirect URI.")]
        public Uri RedirectUri { get; set; }

        //[Display(Name = "Native Client", Description = "Native Client.")]
        //[UIHint("HiddenInput")]
        //public bool NativeClient { get; set; }

        [Display(Name = "Allow Implicit Flow", Description = "Allow implicit flow.")]
        public bool AllowImplicitFlow { get; set; }

        [Display(Name = "Allow Resource Owner Flow", Description = "Allow Resource Owner Flow.")]
        public bool AllowResourceOwnerFlow { get; set; }

        [Display(Name = "Allow Code Flow", Description = "Allow code flow.")]
        public bool AllowCodeFlow { get; set; }

        [Display(Name = "Allow Refresh Tokens", Description = "Allow Refresh Tokens. This enabled offline access for the client to the resource.")]
        public bool AllowRefreshToken { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!HasClientSecret &&
                String.IsNullOrWhiteSpace(this.ClientSecret) &&
                (this.AllowCodeFlow || this.AllowResourceOwnerFlow))
            {
                errors.Add(new ValidationResult("Client Secret is required for Code and Resource Owner Flows.", new string[] { "ClientSecret" }));
            }

            if (this.RedirectUri == null &&
                (this.AllowCodeFlow || this.AllowImplicitFlow))
            {
                errors.Add(new ValidationResult("Redirect URI is required for Code and Implicit Flows.", new string[] { "RedirectUri" }));
            }

            if (this.RedirectUri != null && this.RedirectUri.Scheme == Uri.UriSchemeHttp)
            {
                errors.Add(new ValidationResult("Redirect URI must be HTTPS.", new string[] { "RedirectUri" }));
            }

            return errors;
        }
    }
}
