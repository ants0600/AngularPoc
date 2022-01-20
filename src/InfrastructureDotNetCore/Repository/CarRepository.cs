using Dapper;
using Domain.Model;
using Domain.Repository;
using Domain.Resources;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureDotNetCore.Repository
{
	public class CarRepository : BaseRepository, ICarRepository
	{

		public CarRepository(IConfiguration configuration) : base(configuration)
		{

		}

		public bool DeleteByIds(int[] ids)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.xml] = this.GenerateIdsParameter(ids)
			};
			var s = this._connection.Execute(ConstantValues.QUERY_DELETE_CARS_BY_IDS, parameters, null, int.MaxValue, CommandType.Text);
			return s > 0;
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

		public List<Car> GetAllCars()
		{
			return this._connection.Query<Car>(ConstantValues.QUERY_SELECT_CARS).ToList();
		}

		public Car GetCarById(int id)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = id
			};

			return this._connection.QueryFirstOrDefault<Car>(ConstantValues.QUERY_GET_CAR_BY_ID, parameters, null, int.MaxValue, CommandType.Text);
		}

		public Car GetCarByName(string carName)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = carName
			};

			return this._connection.QueryFirstOrDefault<Car>(ConstantValues.QUERY_GET_CAR_BY_NAME, parameters, null, int.MaxValue, CommandType.Text);
		}

		public bool Insert(Car inserted)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = inserted.Name,
				[DataBaseParameters.price] = inserted.Price,
				[DataBaseParameters.year] = inserted.Year
			};

			var s = this._connection.Execute(ConstantValues.QUERY_INSERT_CAR, parameters, null, int.MaxValue, CommandType.Text);
			return s > 0;
		}

		public List<Car> TestDelayRun()
		{
			return null;
		}

		/// <summary>
		/// update car by id
		/// </summary>
		/// <param name="updated"></param>
		/// <returns></returns>
		public bool Update(Car updated)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = updated.Name,
				[DataBaseParameters.price] = updated.Price,
				[DataBaseParameters.year] = updated.Year,
				[DataBaseParameters.id] = updated.Id
			};
			var s = this._connection.Execute(ConstantValues.QUERY_UPDATE_CAR_BY_ID, parameters, null, int.MaxValue, CommandType.Text);
			return s > 0;
		}

		public List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields sortBy, bool isAscending)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.pageIndex] = pageIndex,
				[DataBaseParameters.pageSize] = pageSize,
				[DataBaseParameters.orderBy] = sortBy.ToString(),
				[DataBaseParameters.isAscending] = isAscending
			};

			return this._connection.Query<Car>(ConstantValues.QUERY_SELECT_CARS_BY_PAGE_INDEX, parameters, null, false, int.MaxValue, CommandType.Text).ToList();
		}

		public int GetCarPageCount(int pageSize)
		{
			var count = this._connection.QueryFirstOrDefault<int>(ConstantValues.QUERY_GET_CARS_COUNT);
			if (pageSize <= 0)
			{
				pageSize = 1;
			}

			var value = count / pageSize;
			if (count % pageSize > 0)
			{
				value++;
			}

			return value;
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
			switch (errorCode)
			{
				case ErrorEnum.CarNameIsEmpty:
					{
						return FieldText.ErrorCarNameIsEmpty;
					}
				case ErrorEnum.CarPriceIsInvalid:
					{
						return FieldText.ErrorCarPriceIsInvalid;
					}
				case ErrorEnum.CarYearIsInvalid:
					{
						return FieldText.ErrorCarYearIsInvalid;
					}
			}

			return string.Empty;
		}
	}
}
