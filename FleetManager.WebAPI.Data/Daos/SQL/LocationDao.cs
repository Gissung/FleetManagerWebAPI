using Dapper;
using FleetManager.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManager.WebAPI.Data.Daos.SQL
{
    // TODO: (Step 4) implement data access object for the Locations table in the SQL Server database

    // 1. make the class inherit from the BaseDao class and use the relevant data context interface as type parameter
    // 2. implement the IDao interface in the class with the Location model class as type parameter

    class LocationDao : BaseDao<IDataContext<IDbConnection>>, IDao<Location>
    {
        public LocationDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public Location Create(Location model)
        {
            string query = "INSERT INTO [LOCATIONS] (name,) VALUES(name=@name,)";
            using IDbConnection connection = DataContext.Open();
            connection.Query<Location>(query, new {  name = model.Name });
            return model;
        }

        public bool Delete(Location model)
        {
            string query = "DELETE FROM LOCATIONS WHERE id=@id ";
            using IDbConnection connection = DataContext.Open();
            var affectedrows = connection.Query<Car>(query, new { id = model.Id });
            return affectedrows.Any();
        }

        public IEnumerable<Location> Read()
        {
            string query = "SELECT * FROM LOCATIONS";
            using IDbConnection connection = DataContext.Open();

            return connection.Query<Location>(query);
        }

        public IEnumerable<Location> Read(Func<Location, bool> predicate)
        {
            string query = "SELECT * FROM CARS ";
            using IDbConnection connection = DataContext.Open();
            return connection.Query<Location>(query).Where(predicate);
        }

        public bool Update(Location model)
        {
            string query = "UPDATE CARS SET name =@ brand WHERE id=id@";
            using IDbConnection connection = DataContext.Open();
            var affectedrows = connection.Query<Location>(query, new { brand = model.Name, id = model.Id });
            return affectedrows.Any();
        }
    }
}
