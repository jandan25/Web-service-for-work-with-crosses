using CrossEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCrosses.Tests.entities.FakeDbsets
{
    class FakeCarModelsSet : FakeDbSet<CarModels>
    {
        public override CarModels Find (params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.CarModelID == (int)keyValues.Single());
        }
    }

    class FakeManufactorsSet : FakeDbSet<Manufactors>
    {
        public override Manufactors Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ManufactorID == (int)keyValues.Single());
        }
    }

    class FakeMotorsSet : FakeDbSet<Motors>
    {
        public override Motors Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.MotorID == (int)keyValues.Single());
        }
    }

    class FakeProductsSet : FakeDbSet<Products>
    {
        public override Products Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ProductID == (int)keyValues.Single());
        }
    }

    class FakeProductsAndMotorsSet : FakeDbSet<ProductsAndMotors>
    {
        public override ProductsAndMotors Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ProductAndMotorID == (int)keyValues.Single());
        }
    }

    class FakeProductsAndOesSet : FakeDbSet<ProductsAndOes>
    {
        public override ProductsAndOes Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ProductAndOeID == (int)keyValues.Single());
        }
    }

    class FakeVenycleTypesSet : FakeDbSet<VenycleTypes>
    {
        public override VenycleTypes Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.VenycleTypeID == (int)keyValues.Single());
        }
    }

    class FakeUserRolesSet : FakeDbSet<UserRoles>
    {
        public override UserRoles Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.UserRoleID == (int)keyValues.Single());
        }
    }

    class FakeUsersSet : FakeDbSet<Users>
    {
        public override Users Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.UserID == (int)keyValues.Single());
        }
    }

    

}
