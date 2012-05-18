﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace no.nith.pj600.dashboard.Code
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database")]
	public partial class DatabaseClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSLAProject(SLAProject instance);
    partial void UpdateSLAProject(SLAProject instance);
    partial void DeleteSLAProject(SLAProject instance);
    partial void InsertTripletexImport(TripletexImport instance);
    partial void UpdateTripletexImport(TripletexImport instance);
    partial void DeleteTripletexImport(TripletexImport instance);
    #endregion
		
		public DatabaseClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DatabaseClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Article> Articles
		{
			get
			{
				return this.GetTable<Article>();
			}
		}
		
		public System.Data.Linq.Table<Balance> Balances
		{
			get
			{
				return this.GetTable<Balance>();
			}
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<Employee> Employees
		{
			get
			{
				return this.GetTable<Employee>();
			}
		}
		
		public System.Data.Linq.Table<Project> Projects
		{
			get
			{
				return this.GetTable<Project>();
			}
		}
		
		public System.Data.Linq.Table<SalesFigure> SalesFigures
		{
			get
			{
				return this.GetTable<SalesFigure>();
			}
		}
		
		public System.Data.Linq.Table<SLAProject> SLAProjects
		{
			get
			{
				return this.GetTable<SLAProject>();
			}
		}
		
		public System.Data.Linq.Table<TripletexImport> TripletexImports
		{
			get
			{
				return this.GetTable<TripletexImport>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].Article")]
	public partial class Article
	{
		
		private string _ArticleNo;
		
		private string _Name;
		
		public Article()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArticleNo", DbType="VarChar(50)")]
		public string ArticleNo
		{
			get
			{
				return this._ArticleNo;
			}
			set
			{
				if ((this._ArticleNo != value))
				{
					this._ArticleNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(100)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].Balance")]
	public partial class Balance
	{
		
		private System.Nullable<int> _Period;
		
		private System.Nullable<int> _Year;
		
		private System.Nullable<double> _Amount;
		
		private System.Nullable<int> _ProjectNo;
		
		private System.Nullable<System.DateTime> _LastUpdate;
		
		public Balance()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Period", DbType="Int")]
		public System.Nullable<int> Period
		{
			get
			{
				return this._Period;
			}
			set
			{
				if ((this._Period != value))
				{
					this._Period = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int")]
		public System.Nullable<int> Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this._Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Float")]
		public System.Nullable<double> Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this._Amount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectNo", DbType="Int")]
		public System.Nullable<int> ProjectNo
		{
			get
			{
				return this._ProjectNo;
			}
			set
			{
				if ((this._ProjectNo != value))
				{
					this._ProjectNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastUpdate", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastUpdate
		{
			get
			{
				return this._LastUpdate;
			}
			set
			{
				if ((this._LastUpdate != value))
				{
					this._LastUpdate = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].Customer")]
	public partial class Customer
	{
		
		private System.Nullable<int> _CustomerNo;
		
		private string _Name;
		
		public Customer()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerNo", DbType="Int")]
		public System.Nullable<int> CustomerNo
		{
			get
			{
				return this._CustomerNo;
			}
			set
			{
				if ((this._CustomerNo != value))
				{
					this._CustomerNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].Employee")]
	public partial class Employee
	{
		
		private System.Nullable<int> _EmployeeNo;
		
		private string _Name;
		
		public Employee()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeNo", DbType="Int")]
		public System.Nullable<int> EmployeeNo
		{
			get
			{
				return this._EmployeeNo;
			}
			set
			{
				if ((this._EmployeeNo != value))
				{
					this._EmployeeNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].Project")]
	public partial class Project
	{
		
		private System.Nullable<int> _ProjectNo;
		
		private string _Name;
		
		private System.Nullable<System.DateTime> _StartTime;
		
		private System.Nullable<System.DateTime> _StopTime;
		
		private System.Nullable<int> _CustomerNo;
		
		private System.Nullable<int> _PMEmployeeNo;
		
		public Project()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectNo", DbType="Int")]
		public System.Nullable<int> ProjectNo
		{
			get
			{
				return this._ProjectNo;
			}
			set
			{
				if ((this._ProjectNo != value))
				{
					this._ProjectNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if ((this._StartTime != value))
				{
					this._StartTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StopTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> StopTime
		{
			get
			{
				return this._StopTime;
			}
			set
			{
				if ((this._StopTime != value))
				{
					this._StopTime = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerNo", DbType="Int")]
		public System.Nullable<int> CustomerNo
		{
			get
			{
				return this._CustomerNo;
			}
			set
			{
				if ((this._CustomerNo != value))
				{
					this._CustomerNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PMEmployeeNo", DbType="Int")]
		public System.Nullable<int> PMEmployeeNo
		{
			get
			{
				return this._PMEmployeeNo;
			}
			set
			{
				if ((this._PMEmployeeNo != value))
				{
					this._PMEmployeeNo = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[99x].SalesFigures")]
	public partial class SalesFigure
	{
		
		private System.Nullable<double> _TotalSalesAmount;
		
		private System.Nullable<int> _Period;
		
		private System.Nullable<int> _Year;
		
		private System.Nullable<int> _ProjectNo;
		
		private string _ArticleNo;
		
		public SalesFigure()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TotalSalesAmount", DbType="Float")]
		public System.Nullable<double> TotalSalesAmount
		{
			get
			{
				return this._TotalSalesAmount;
			}
			set
			{
				if ((this._TotalSalesAmount != value))
				{
					this._TotalSalesAmount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Period", DbType="Int")]
		public System.Nullable<int> Period
		{
			get
			{
				return this._Period;
			}
			set
			{
				if ((this._Period != value))
				{
					this._Period = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Year", DbType="Int")]
		public System.Nullable<int> Year
		{
			get
			{
				return this._Year;
			}
			set
			{
				if ((this._Year != value))
				{
					this._Year = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectNo", DbType="Int")]
		public System.Nullable<int> ProjectNo
		{
			get
			{
				return this._ProjectNo;
			}
			set
			{
				if ((this._ProjectNo != value))
				{
					this._ProjectNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArticleNo", DbType="VarChar(50)")]
		public string ArticleNo
		{
			get
			{
				return this._ArticleNo;
			}
			set
			{
				if ((this._ArticleNo != value))
				{
					this._ArticleNo = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SLAProjects")]
	public partial class SLAProject : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _ProjectNo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnProjectNoChanging(int value);
    partial void OnProjectNoChanged();
    #endregion
		
		public SLAProject()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectNo", DbType="Int NOT NULL")]
		public int ProjectNo
		{
			get
			{
				return this._ProjectNo;
			}
			set
			{
				if ((this._ProjectNo != value))
				{
					this.OnProjectNoChanging(value);
					this.SendPropertyChanging();
					this._ProjectNo = value;
					this.SendPropertyChanged("ProjectNo");
					this.OnProjectNoChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TripletexImport")]
	public partial class TripletexImport : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _ProjectNo;
		
		private string _ProjectName;
		
		private string _ProjectLeader;
		
		private string _DepName;
		
		private string _EmployeeName;
		
		private System.DateTime _Date;
		
		private double _Hours;
		
		private string _Comment;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnProjectNoChanging(int value);
    partial void OnProjectNoChanged();
    partial void OnProjectNameChanging(string value);
    partial void OnProjectNameChanged();
    partial void OnProjectLeaderChanging(string value);
    partial void OnProjectLeaderChanged();
    partial void OnDepNameChanging(string value);
    partial void OnDepNameChanged();
    partial void OnEmployeeNameChanging(string value);
    partial void OnEmployeeNameChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnHoursChanging(double value);
    partial void OnHoursChanged();
    partial void OnCommentChanging(string value);
    partial void OnCommentChanged();
    #endregion
		
		public TripletexImport()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectNo", DbType="Int NOT NULL")]
		public int ProjectNo
		{
			get
			{
				return this._ProjectNo;
			}
			set
			{
				if ((this._ProjectNo != value))
				{
					this.OnProjectNoChanging(value);
					this.SendPropertyChanging();
					this._ProjectNo = value;
					this.SendPropertyChanged("ProjectNo");
					this.OnProjectNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProjectName
		{
			get
			{
				return this._ProjectName;
			}
			set
			{
				if ((this._ProjectName != value))
				{
					this.OnProjectNameChanging(value);
					this.SendPropertyChanging();
					this._ProjectName = value;
					this.SendPropertyChanged("ProjectName");
					this.OnProjectNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectLeader", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProjectLeader
		{
			get
			{
				return this._ProjectLeader;
			}
			set
			{
				if ((this._ProjectLeader != value))
				{
					this.OnProjectLeaderChanging(value);
					this.SendPropertyChanging();
					this._ProjectLeader = value;
					this.SendPropertyChanged("ProjectLeader");
					this.OnProjectLeaderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string DepName
		{
			get
			{
				return this._DepName;
			}
			set
			{
				if ((this._DepName != value))
				{
					this.OnDepNameChanging(value);
					this.SendPropertyChanging();
					this._DepName = value;
					this.SendPropertyChanged("DepName");
					this.OnDepNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string EmployeeName
		{
			get
			{
				return this._EmployeeName;
			}
			set
			{
				if ((this._EmployeeName != value))
				{
					this.OnEmployeeNameChanging(value);
					this.SendPropertyChanging();
					this._EmployeeName = value;
					this.SendPropertyChanged("EmployeeName");
					this.OnEmployeeNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Hours", DbType="Float NOT NULL")]
		public double Hours
		{
			get
			{
				return this._Hours;
			}
			set
			{
				if ((this._Hours != value))
				{
					this.OnHoursChanging(value);
					this.SendPropertyChanging();
					this._Hours = value;
					this.SendPropertyChanged("Hours");
					this.OnHoursChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comment", DbType="VarChar(50)")]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if ((this._Comment != value))
				{
					this.OnCommentChanging(value);
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
					this.OnCommentChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
