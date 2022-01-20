using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Model;
using Domain.Repository;
using Domain.Resources;
using Domain.Service;

namespace Infrastructure.Repository
{
	public class CarRepository : BaseRepository, ICarRepository
	{
		public CarRepository(IConnectionStrings connectionStrings, IQueryHelpers helper) : base(connectionStrings, helper)
		{
		}

		public List<Car> GetAllCars()
		{
			return this._helper.GetListFromQuery<Car>(this._connection, ConstantValues.QUERY_SELECT_CARS, CommandType.Text,
				int.MaxValue, false);
		}

		public bool Insert(Car inserted)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = inserted.Name,
				[DataBaseParameters.price] = inserted.Price,
				[DataBaseParameters.year] = inserted.Year
			};

			var executeNonQuery = this._helper.ExecuteNonQuery(this._connection, ConstantValues.QUERY_INSERT_CAR, parameters,
				CommandType.Text,
				int.MaxValue);
			return executeNonQuery > 0;
		}

		public bool Update(Car updated)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = updated.Name,
				[DataBaseParameters.price] = updated.Price,
				[DataBaseParameters.year] = updated.Year,
				[DataBaseParameters.id] = updated.Id
			};
			return this._helper.ExecuteNonQuery(this._connection, ConstantValues.QUERY_UPDATE_CAR_BY_ID, parameters,
					   CommandType.Text, int.MaxValue) > 0;
		}

		public Car GetCarByName(string carName)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = carName
			};
			var listFromQuery = this._helper.GetListFromQuery<Car>(this._connection, ConstantValues.QUERY_GET_CAR_BY_NAME,
				parameters, CommandType.Text, int.MaxValue, false);
			var value = listFromQuery?.FirstOrDefault();
			return value;
		}

		public Car GetCarById(int id)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = id
			};
			var listFromQuery = this._helper.GetListFromQuery<Car>(this._connection, ConstantValues.QUERY_GET_CAR_BY_ID,
				parameters, CommandType.Text, int.MaxValue, false);
			var value = listFromQuery?.FirstOrDefault();
			return value;
		}

		public List<Car> TestDelayRun()
		{
			return this._helper.GetListFromQuery<Car>(this._connection, ConstantValues.QUERY_DELAY_TEST, CommandType.Text,
				int.MaxValue, false);
		}

		public bool DeleteByIds(int[] ids)
		{
			if (ids == null)
			{
				return false;
			}

			if (ids.Length <= 0)
			{
				return false;
			}

			var xml = this.GenerateIdsParameter(ids);
			var parameters = new Dictionary<string, object> { [DataBaseParameters.xml] = xml };

			return this._helper.ExecuteNonQuery(this._connection, ConstantValues.QUERY_DELETE_CARS_BY_IDS, parameters,
					   CommandType.Text, int.MaxValue) > 0;
		}

		protected string GenerateIdsParameter(int[] ids)
		{
			/*
DECLARE @xml XML
SET @xml = '
<rows>
	<row>
		<Id>0</Id>
	</row>
	<row>
		<Id>0</Id>
	</row>
</rows>

'

EXEC dbo.usp_DeleteCars @xml = @xml

//*/

			var idXmlRows = ids.Select(i => string.Format(ConstantValues.ID_ROW_XML_FORMAT, i)).ToArray();
			var idXml = string.Join(string.Empty, idXmlRows);
			var value = string.Format(ConstantValues.ROWS_XML_FORMAT, idXml);
			return value;
		}

		public List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields sortBy, bool isAscending)
		{
			throw new System.NotImplementedException();
		}

		public int GetCarPageCount(int pageSize)
		{
			throw new System.NotImplementedException();
		}

		public ErrorEnum Validate(Car inserted)
		{
			inserted.Name = $"{inserted.Name}".Trim();
			if (string.IsNullOrEmpty(inserted.Name))
			{
				return ErrorEnum.CarNameIsEmpty;
			}

			if (inserted.Price <= 0)
			{
				return ErrorEnum.CarPriceIsInvalid;
			}

			bool isInvalidYear = inserted.Year <= 0 || inserted.Year > DateTime.Today.Year;
			if (isInvalidYear)
			{
				return ErrorEnum.CarYearIsInvalid;
			}

			return ErrorEnum.None;
		}

		public string GetErrorMessage(ErrorEnum errorCode)
		{
			throw new System.NotImplementedException();
		}
	}
}