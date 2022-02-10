using CustomerAPI.Model;
using CustomerAPI.Repository;
using CustomerAPI.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerAPI.Tests
{
    public class CustomerServiceUnitTest
    {
        Mock<IDBManager> moqDBManager;
        List<Customer> customerList;
        public CustomerServiceUnitTest()
        {
            moqDBManager = new Mock<IDBManager>();
            customerList = new List<Customer> { new Customer { Id = 1, firstName = "Lorem", lastName = "Ipsum", dob = "01/01/2000" },
                                            new Customer { Id = 2, firstName = "testfirstname", lastName = "testlastname", dob = "1/1/1980" }};
        }
        [Fact]
        public void Successfull_AddCustomer_ReturnTrue()
        {
            //given
            moqDBManager.Setup(m => m.GetRepository<Customer>().Add(customerList[1]));
            moqDBManager.Setup(m => m.SaveAsync()).ReturnsAsync(1);

            var sut = new CustomerService(moqDBManager.Object);

            //when
            var result = sut.AddCustomer(customerList[1]);

            //then
            Assert.True(result.Result);
        }

        [Fact]
        public void FailSaveDb_WhenAddCustomer_ReturnFalse()
        {
            //given
            moqDBManager.Setup(m => m.GetRepository<Customer>().Add(customerList[1]));
            moqDBManager.Setup(m => m.SaveAsync()).ReturnsAsync(0);

            var sut = new CustomerService(moqDBManager.Object);

            //when
            var result = sut.AddCustomer(customerList[1]);

            //then
            Assert.False(result.Result);
        }

        [Fact]
        public void Exception_WhenAddCustomer_ReturnFalse()
        {
            //given
            moqDBManager.Setup(m => m.GetRepository<Customer>().Add(customerList[1]));
            moqDBManager.Setup(m => m.SaveAsync()).Throws(new Exception());

            var sut = new CustomerService(moqDBManager.Object);

            //when
            var result = sut.AddCustomer(customerList[1]);

            //then
            Assert.False(result.Result);
        }

        [Fact]
        public void FindCustomer_Return_CustomerDetails()
        {
            //given
            moqDBManager.Setup(m => m.GetRepository<Customer>().Get()).Returns(customerList);

            var sut = new CustomerService(moqDBManager.Object);

            //when
            var result = sut.FindCustomer("lorem");

            //then
            Assert.Equal(customerList[0], result.Result);
        }
    }
}
