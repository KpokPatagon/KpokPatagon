using KpokPatagon.Data;
using KpokPatagon.Data.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KpokPatagon.Multitenancy
{
    /// <summary>
    /// <b>TenantDataService</b> implements the data services tenants table.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This service does not have any configuration for the services required for it to work.
    ///     </para>
    ///     <para>
    ///         All required services like the <b>CommandBuilder</b> and <b>DataModelCommandBuilder</b>
    ///         must be configured by a <b>DataServiceOptions&lt;TenantDataService&gt;</b> 
    ///         configuration within the dependency injection service collection.
    ///     </para>
    /// </remarks>
    public class TenantDataService : DataService
    {
        /// <summary>
        /// Creates a new instance of <see cref="TenantDataService"/>.
        /// </summary>
        public TenantDataService()
        {
            Schema = MultitenancyConsts.HostSchema;
            SequenceName = null;
        }


        string _alias;
        /// <summary>
        /// The alias for the underlaying table.
        /// </summary>
        public override string Alias
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_alias))
                    _alias = "Tenant";
                return _alias;
            }
            set { _alias = value; }
        }

        string _table;
        /// <summary>
        /// The name of the underlaying table.
        /// </summary>
        public override string Table
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_table))
                    _table = "Tenant";
                return _table;
            }
            set { _table = value; }
        }

        IDictionary<string, Field> _fields;
        /// <summary>
        /// A dictionary of field configuration for the underlaying table.
        /// </summary>
        public override IDictionary<string, Field> Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new Dictionary<string, Field>()
                    {
                        ["Id"] = new Field("Id", DbType.Integer, 1, AutomaticType.None),
                        ["Ring"] = new Field("Ring", DbType.Integer, 0, AutomaticType.None),
                        ["Name"] = new Field("Name", DbType.Text, 0, AutomaticType.None),
                        ["DisplayName"] = new Field("DisplayName", DbType.NText, 0, AutomaticType.None),
                        ["Email"] = new Field("Email", DbType.NText, 0, AutomaticType.None),
                        ["Culture"] = new Field("Culture", DbType.Text, 0, AutomaticType.None),
                        ["MailServerId"] = new Field("MailServerId", DbType.Text, 0, AutomaticType.None),
                        ["MailFromAddress"] = new Field("MailFromAddress", DbType.NText, 0, AutomaticType.None),
                        ["MailFromName"] = new Field("MailFromName", DbType.NText, 0, AutomaticType.None),
                        ["ConnectionString"] = new Field("ConnectionString", DbType.Text, 0, AutomaticType.None),
                        ["ProvisionState"] = new Field("ProvisionState", DbType.Text, 0, AutomaticType.None),
                        ["SubscriptionState"] = new Field("SubscriptionState", DbType.Text, 0, AutomaticType.None),
                        ["SubscriptionStateComment"] = new Field("SubscriptionStateComment", DbType.NClob, 0, AutomaticType.None),
                        ["TrialDurationInDays"] = new Field("TrialDurationInDays", DbType.Integer, 0, AutomaticType.None),
                        ["TrialStarted"] = new Field("TrialStarted", DbType.DateTime, 0, AutomaticType.None),
                        ["TrialEnd"] = new Field("TrialEnd", DbType.DateTime, 0, AutomaticType.None),
                        ["DeletionTime"] = new Field("DeletionTime", DbType.DateTime, 0, AutomaticType.None)
                    };
                }
                return _fields;
            }
            set { _fields = value; }
        }

        /// <summary>
        /// Gets a collection of all registered tenants.
        /// </summary>
        /// <returns>
        ///     A collection of <see cref="Tenant"/>s.
        /// </returns>
        public IList<Tenant> GetAll()
        {
            EnsureState();
            return BuildModelList<Tenant>(new SelectStatement(this));
        }

        /// <summary>
        /// Gets a collection of all registered tenants.
        /// </summary>
        /// <returns>
        ///     A collection of <see cref="Tenant"/>s.
        /// </returns>
        public Task<IList<Tenant>> GetAllAsync() => Task.FromResult(GetAll());

        /// <summary>
		/// Gets a tenant looking by it's name without considering the case.
		/// </summary>
		/// <param name="name">Tenant name to look for.</param>
		/// <returns>
        ///     A <see cref="Tenant"/> entity or <see langword="null"/> if not found.
        /// </returns>
		public Tenant GetTenantByNameCaseInsensitive(string name)
        {
            EnsureState();

            SelectStatement s;
            s = new SelectStatement(this);
            s.WhereConditions.Add(new SearchCondition(
                new SimplePredicate(new PredicateColumn(Fields["Name"]), ComparisonOperator.Equal, name, false)));

            return BuildModelList<Tenant>(s).FirstOrDefault();
        }

        /// <summary>
        /// Gets a collection of registered tenants that has an exclusive database configured.
        /// </summary>
        /// <returns>
        ///     A collection of <see cref="Tenant"/>s.
        /// </returns>
        public IList<Tenant> GetTenantsWithExclusiveDatabase()
        {
            EnsureState();

            SelectStatement s;
            s = new SelectStatement(this);
            s.WhereConditions.Add(new SearchCondition(
                new SimplePredicate(new PredicateColumn(Fields["ConnectionString"]), ComparisonOperator.NotEqual, null)
            ));

            return BuildModelList<Tenant>(s);
        }

        /// <summary>
        /// Gets a collection of registered tenants that has an exclusive database configured.
        /// </summary>
        /// <returns>
        ///     A collection of <see cref="Tenant"/>s.
        /// </returns>
        public Task<IList<Tenant>> GetTenantsWithExclusiveDatabaseAsync() => Task.FromResult(GetTenantsWithExclusiveDatabase());
    }
}