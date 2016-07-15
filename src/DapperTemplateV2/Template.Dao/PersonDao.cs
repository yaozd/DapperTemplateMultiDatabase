
using System;
    using System.Collections.Generic;
    using System.Text;
    using DapperExt;
    using Template.Model;

    namespace Template.Dao
    {
    public class PersonDao
    {
    private readonly DbHelperSql _dbHelper;
    public PersonDao ()
    {
    _dbHelper = SqlMapper.Instance.GetDbHelperSql(EnumDbName.Test1);
    }
    //

    public int Add(Person model)
    {
    StringBuilder strSql = new StringBuilder();
    strSql.Append("insert into [Person](");
    strSql.Append("username,password,age,registerDate,address)");
    strSql.Append(" values (");
    strSql.Append("@username,@password,@age,@registerDate,@address)");
    //
    var id= _dbHelper.InsertReturnId(strSql.ToString(), model);
    return id;
    }
    //

    public bool DeleteById( Int32 id )
    {
    StringBuilder strSql = new StringBuilder();
    strSql.Append("delete from [Person] ");
    strSql.Append("where  id=@id  ");
    //
    var flag= _dbHelper.Delete(strSql.ToString(), new Person {  Id=id  });
    return flag;
    }
    //

    public bool UpdateById(Person model,  Int32 id )
    {
            model.Id = id;
            StringBuilder strSql = new StringBuilder();
    strSql.Append("update [Person] set ");
            if(model.@Username!=null)
                strSql.Append("username=@Username,");
            if(model.@Password!=null)
                strSql.Append("password=@Password,");
            if(model.@Age!=null)
                strSql.Append("age=@Age,");
            if(model.@RegisterDate!=null)
                strSql.Append("registerDate=@RegisterDate,");
            if(model.@Address!=null)
                strSql.Append("address=@Address,");
            int n = strSql.ToString().LastIndexOf(",", StringComparison.Ordinal);
    if(n>0)strSql.Remove(n, 1);
    strSql.Append(" where  id=@id  ");
    var flag = _dbHelper.Update(strSql.ToString(), model);
    return flag;
    }
    //

    public Person FindById( Int32 id )
    {
    //Ä¬ÈÏtop 1--1Ìõ¼ÇÂ¼
    StringBuilder strSql = new StringBuilder();
    strSql.Append("select  top 1 ");
    strSql.Append("id,username,password,age,registerDate,address  ");
    strSql.Append("from [Person] ");
    strSql.Append("where  id=@id  ");
    //
    var result = _dbHelper.Find(strSql.ToString(), new Person {  Id=id  });
    return result;
    }
    //
    public IList<Person>FindList(Person whereModel, string where, int top)
        {
        StringBuilder strSql = new StringBuilder();
        strSql.AppendFormat("select top {0} ",top);
        strSql.Append("id,username,password,age,registerDate,address  ");
        strSql.Append("from [Person] ");
        strSql.AppendFormat("where 1=1 and {0} ", where);
        var result = _dbHelper.FindList<Person>
    (strSql.ToString(), whereModel);
    return result;
    }
    //
    public IList<Person> FindListByPage(Person whereModel, string where, string orderBy, int pageIndex, int pageSize)
        {
        var startIndex = pageIndex * pageSize;
        var size = pageSize;
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        strSql.Append("id,username,password,age,registerDate,address ");
        strSql.Append("from [Person] ");
        strSql.AppendFormat("where 1=1 and {0} ", where);
        strSql.AppendFormat("ORDER BY {0} OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY", orderBy, startIndex, size);
        var result = _dbHelper.FindList<Person>(strSql.ToString(), whereModel);
        return result;
    }
    //
    public int Count(Person whereModel, string where)
    {
    StringBuilder strSql = new StringBuilder();
    strSql.Append("select  count(*) from [Person] ");
    strSql.AppendFormat("where 1=1 and {0} ", where);
    var result = _dbHelper.Count(strSql.ToString(), whereModel);
    return result;
    }
    //
    }
    }
