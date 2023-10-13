using NotificationManagement.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManagement.Domain.Entities.ReceiverAggregate;

public class ReceiverInfo : Entity, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private int _phoneNumber;
    private string _email;


    public ReceiverInfo(string firstName, string lastName, int phoneNumber, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        _phoneNumber = phoneNumber;
        _email = email;
    }



    public void UpdateInformation(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdatePhoneNumber(int phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public void UpdateEmail(string email)
    {
        _email = email;
    }
}
