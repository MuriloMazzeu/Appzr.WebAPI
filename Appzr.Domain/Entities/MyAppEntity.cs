using Appzr.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Appzr.Domain.Entities
{
    public sealed class MyAppEntity : EntityBase
    {
        public string Name { get; private set; }
        public string Link { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public MyAppEntity() : this(null)
        {
        }

        public MyAppEntity(Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            CreatedAt = DateTime.Now;
        }

        public MyAppEntity(Guid id, string name, string link, DateTime createdAt) : this(id)
        {
            Name = name;
            Link = link;
            CreatedAt = createdAt;
        }

        private bool ValidateName(string name, out IEnumerable<string> errors)
        {
            var _errors = new List<string>();

            if (string.IsNullOrWhiteSpace(name))
                _errors.Add("O nome informado é invalido");

            var nameLength = name.Length;
            if (nameLength < 3)
                _errors.Add("O nome informado é muito pequeno");

            if (nameLength > 100)
                _errors.Add("O nome informado é muito grande");

            errors = _errors;
            return _errors.Count == 0;
        }

        public void ChangeName(string name)
        {
            if(ValidateName(name, out var errors))
            {
                Name = name;
            }
            else
            {
                var message = "Nome invalido: " + string.Join(", ", errors);
                throw new ArgumentException(message, "name");
            }
        }

        private bool ValidateLink(string link, out IEnumerable<string> errors)
        {
            var _errors = new List<string>();

            if (string.IsNullOrWhiteSpace(link))
                _errors.Add("O link informado é invalido");

            var linkLength = link.Length;
            if (linkLength < 10)
                _errors.Add("O link informado é muito pequeno");

            if (linkLength > 1000)
                _errors.Add("O link informado é muito grande");

            errors = _errors;
            return _errors.Count == 0;
        }

        public void ChangeLink(string link)
        {
            if (ValidateLink(link, out var errors))
            {
                Link = link;
            }
            else
            {
                var message = "Link invalido: " + string.Join(", ", errors);
                throw new ArgumentException(message, "name");
            }
        }
    }
}
