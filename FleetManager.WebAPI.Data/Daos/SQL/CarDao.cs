using FleetManager.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FleetManager.WebAPI.Data.Daos.SQL
{
    // TODO: (Step 4) implement data access object for the Cars table in the SQL Server database

    // 1. make the class inherit from the BaseDao class and use the relevant data context interface as type parameter
    // 2. implement the IDao interface in the class with the Car model class as type parameter

    class CarDao : BaseDao<IDataContext<IDbConnection>>, IDao<Car>
    {
        public CarDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {

        }
        public Car Create(Car model)
        {
            string query = "INSERT INTO [CARS] (brand,mileage) VALUES(@brand,@mileage)";
            using IDbConnection connection = DataContext.Open();
            connection.Query<Car>(query,new { brand = model.Brand, mileage = model.Mileage} );
            return model;
        }

        public bool Delete(Car model)
        {
            string query = "DELETE FROM CARS WHERE id=@id ";
            using IDbConnection connection = DataContext.Open();
           var affectedrows = connection.Query<Car>(query, new { id = model.Id }); 
            return affectedrows.Any();

        }

        public IEnumerable<Car> Read()
        {
            string query = "SELECT * FROM CARS";
           using IDbConnection connection = DataContext.Open();

            return connection.Query<Car>(query);
            
        }

        public IEnumerable<Car> Read(Func<Car, bool> predicate)
        {
            string query = "SELECT * FROM CARS ";
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Car>(query).Where(predicate);
        }

        public bool Update(Car model)
        {
            string query = "UPDATE CARS SET brand =@ brand WHERE id=id@";
            using IDbConnection connection = DataContext.Open();
            var affectedrows = connection.Query<Car>(query, new {brand = model.Brand, id = model.Id });
            return affectedrows.Any();
            
        }
    }


}
