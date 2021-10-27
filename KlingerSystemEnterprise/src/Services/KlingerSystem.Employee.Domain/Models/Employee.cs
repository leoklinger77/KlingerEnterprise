using KlingerSystem.Employee.Domain.Models.Enum;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Employee.Domain.Message;

namespace KlingerSystem.Employee.Domain.Models
{
    public class Employee : Entity, IAggregateRoot
    {   
        public static int COMMISSION_MAX => 100;
        public static int COMMISSION_MIN => 0;
        public static int PHONE_COUNT_MAX => 3;


        public Guid BusinessId { get; private set; }
        public string FullName { get; private set; }
        public Cpf Cpf { get; private set; }
        public Rg Rg { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string ImagePath { get; private set; }
        public double? Commission { get; private set; }
        public string Annotation { get; private set; }
        public EmployeeType EmployeeType { get; private set; }
        public TypeSexo TypeSexo { get; private set; }

        public Email Email { get; private set; }
        public Address Address { get; private set; }

        private List<Phone> _phones = new List<Phone>();
        public IReadOnlyCollection<Phone> Phones => _phones;


        protected Employee() { }
        protected Employee(Guid id, Guid businessId, string fullName)
        {
            Id = id;
            BusinessId = businessId;
            FullName = fullName;
            IsValid();
        }
        protected Employee(Guid businessId, string fullName,
                            Cpf cpf = null, Rg rg = null, DateTime? birthDate = null,
                            string imagePath = null, double? commission = null,
                            string annotation = null, TypeSexo typeSexo = default)
        {
            BusinessId = businessId;
            FullName = fullName;            
            
            AddCpf(cpf);
            AddRg(rg);
            SetTypeSexo(typeSexo);

            if (imagePath != null) SetImagePath(imagePath);
            if (annotation != null) SetAnnotation(annotation);            
            if (birthDate.HasValue) SetBirthDate(birthDate.Value);
            if (commission.HasValue) SetCommission(commission.Value);

            IsValid();
        }

        public static class EmployeeFactory
        {
            public static Employee EmployeeAdministrator(Guid id, Guid businessId, string fullName)
            {
                var employee = new Employee(id, businessId, fullName);
                employee.EmployeeAdministrator();
                return employee;
            }

            public static Employee EmployeeCommom(Guid businessId, string fullName,
                            Cpf cpf = null, Rg rg = null, DateTime? birthDate = null,
                            string imagePath = null, double? commission = null,
                            string annotation = null, TypeSexo typeSexo = default)
            {
                var employee = new Employee(businessId, fullName, cpf, rg, birthDate, imagePath, commission, annotation, typeSexo);
                employee.EmployeeCommom();
                return employee;
            }
        }


        public void AddCpf(Cpf cpf)
        {
            if (Cpf != null) throw new DomainException(ListEmployeeMessages.CPF_ALTERACAO_ERRO_MSG);
            Cpf = cpf;
        }
        public void AddRg(Rg rg)
        {
            if (Rg != null) throw new DomainException(ListEmployeeMessages.RG_ALTERACAO_ERRO_MSG);
            Rg = rg;
        }
        public void AddPhone(Phone phone)
        {
            Validation.ValidateIfEqual(_phones.Count, PHONE_COUNT_MAX, ListEmployeeMessages.PHONE_COUNT_MAX_ERRO_MSG);
            Validation.ValidateIfTrue(_phones.FirstOrDefault(x => x.Number == phone.Number) != null, ListEmployeeMessages.NUMBER_REPIT_ERRO_MSG);
            _phones.Add(phone);
        }

        public void SetBirthDate(DateTime date)
        {
            if (BirthDateValid(date)) throw new DomainException(ListEmployeeMessages.BIRTHDATE_ERRO_MSG);
            BirthDate = date;
        }

        
        public void SetTypeSexo(TypeSexo type)
        {
            Validation.ValidateIfFalse(type.IsEnum<TypeSexo>(), ListEmployeeMessages.TYPOSEXO_ERRO_MSG);
            TypeSexo = type;
        }
        public void SetImagePath(string path)
        {
            Validation.ValidateIsNullOrEmpty(path, ListEmployeeMessages.IMAGEPATH_ERRO_MSG);
            Validation.ValidateIfContains(path, Guid.Empty.ToString(), ListEmployeeMessages.IMAGEPATH_ERRO_MSG);

            ImagePath = path;
        }
        public void SetCommission(double commission)
        {
            Validation.ValidateMinMax(commission, 0, 100, ListEmployeeMessages.COMMISSION_ERRO_MSG);
            Commission = commission / 100;
        }
        public void SetEmail(Email email)
        {
            Email = email;
        }
        public void SetAddress(Address address)
        {
            Address = address;
        }
        public void SetAnnotation(string annotation)
        {
            Validation.ValidateIsNullOrEmpty(annotation, ListEmployeeMessages.ANNOTATION_REPIT_ERRO_MSG);
            Annotation = annotation;
        }

        public override void IsValid()
        {
            Validation.ValidateIfEqual(Guid.Empty, Id, "");
            Validation.ValidateIfEqual(Guid.Empty, BusinessId, "");
            Validation.ValidateIsNullOrEmpty(FullName, ListEmployeeMessages.FULLNAME_ERRO_MSG);
            Validation.ValidateIfFalse(FullName.IsFullName(), ListEmployeeMessages.FULLNAME_ERRO_MSG);
        }
        public static bool BirthDateValid(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
        private void EmployeeAdministrator()
        {
            EmployeeType = EmployeeType.Administratior;
        }
        private void EmployeeCommom()
        {
            EmployeeType = EmployeeType.Commum;
        }
    }
}
