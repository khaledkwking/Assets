using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

namespace Utilities
{
    public class DBOperation
    {
        //  ("user id=sa;data source=.;persist security info=True;initial catalog=Exchanges;password=sa");
        public string strCon { get; set; }
        public static Logger _Logger;
        public DBOperation(string _strCon)
        {
            strCon = _strCon;

        }
        public DBOperation(string _strCon, string _strConLogger)
        {
            strCon = _strCon;

            _Logger = new Logger(_strConLogger);

        }

        public DBOperation()
        {
        }
        public DataTable GetDataTableFromSql(string strSQL)
        {
            DataTable objDT = new DataTable();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL.ToString(), sqlCon))
                    {
                        Command.CommandText = strSQL;
                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(Command);
                        objSqlDataAdapter.Fill(objDT);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //  _Logger.LogIt(ex.ToString(), enumLOGType.ERROR.ToString());   
            }
            return objDT;
        }
        public void InsertOSSDTtoSQL(DataTable DT)
        {

            //Get the ID from table 
            string strSQL = "select " + DT.Columns[0].ColumnName + " from " + DT.TableName +
                " where  " + DT.Columns[0].ColumnName + " = " + DT.Columns[0] +
                " and DATEDIFF(dd, 0, snapshotDate ) =DATEDIFF(dd, 0, '" +
                DateFromat.AdjustDateToSQL(DateTime.Now.Date) + "')";

            string strRequestID = getDataFromSQlExecuteScalar(strSQL);
            if (strRequestID != string.Empty)
            {
                //    UpdateToSQlFromDatatable(DT , "RequestID");
                //Update
            }
            else
            {
                //Insert 
                // InsertToSQlFromDatatable(DT);

            }
        }

        public void ExecuteSQLQuery(StringBuilder strSQL, string ExchangeID)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL.ToString(), sqlCon))
                    {
                        Command.ExecuteNonQuery();
                    }

                }
            }
            catch (System.Exception ex)
            {

                _Logger.LogIt(strSQL.ToString() + " _ " + ex.ToString(), GrowthConstant.enumLOGType.ERROR.ToString());
            }
        }
        public void ExecuteSQLQuery(StringBuilder strSQL)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL.ToString(), sqlCon))
                    {
                        Command.ExecuteNonQuery();
                    }

                }
            }
            catch (System.Exception ex)
            {

                _Logger.LogIt(ex.ToString(), Utilities.GrowthConstant.enumLOGType.ERROR.ToString());
            }
        }
        public void ExecuteSQLQuery(string strSQL)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL, sqlCon))
                    {
                        Command.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                _Logger.LogIt(ex.ToString(), Utilities.GrowthConstant.enumLOGType.ERROR.ToString());
            }
        }

        public string getDataFromSQlExecuteScalar(string strSQL, string strCon)
        {
            object objResult = null;
            string result = string.Empty;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL, sqlCon))
                    {
                        objResult = Command.ExecuteScalar();
                    }
                }
                if (objResult != null)
                {
                    result = objResult.ToString();
                }

            }
            catch (System.Exception ex)
            {
                _Logger.LogIt(ex.ToString(), GrowthConstant.enumLOGType.ERROR.ToString());
            }
            return result;
        }

        public string getDataFromSQlExecuteScalar(string strSQL)
        {
            object objResult = null;
            string result = string.Empty;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(strCon))
                {
                    sqlCon.Open();
                    using (SqlCommand Command = new SqlCommand("SET DATEFORMAT dmy;" + strSQL, sqlCon))
                    {
                        objResult = Command.ExecuteScalar();
                    }
                }
                if (objResult != null)
                {
                    result = objResult.ToString();
                }

            }
            catch (System.Exception ex)
            {
                _Logger.LogIt(ex.ToString(), GrowthConstant.enumLOGType.ERROR.ToString());
            }
            return result;
        }

        public void InsertToSQlFromDatatable(DataTable DT, string ExchangeID, bool IsEnglish)
        {
            string strSQlTableName = DT.TableName;
            try
            {
                //Remove Null rows from datatable
                //<
                List<DataRow> listDRDeleted = DT.AsEnumerable().ToList<DataRow>();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    listDRDeleted = (from DataRow dr in listDRDeleted
                                     where ISNull(dr[i].ToString())
                                     select dr).ToList<DataRow>();
                }
                foreach (DataRow DR in listDRDeleted)
                {
                    DT.Rows.Remove(DR);
                }
                //>

                if ((DT != null))
                {
                    string strDataCell = null;
                    StringBuilder strSQlInsert = new StringBuilder();

                    foreach (DataRow DR in DT.Rows)
                    {

                        strSQlInsert.Append("insert into " + strSQlTableName);
                        strSQlInsert.Append(" ( ");
                        foreach (DataColumn DC in DT.Columns)
                        {
                            //write Column names
                            strSQlInsert.Append(DC.ColumnName + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) values (");


                        //write cells values
                        foreach (DataColumn DC in DT.Columns)
                        {
                            strDataCell = DR[DC].ToString();

                            if (((System.Type)DC.DataType).FullName == "System.DateTime")
                            {
                                strDataCell = DateFromat.AdjustDateToSQL(strDataCell);
                            }

                            if (((System.Type)DC.DataType).FullName == "System.Int32")
                            {
                                strDataCell = (string)GetStrZeroIfNull(strDataCell);
                            }

                            strDataCell = (string)GetStrNull(strDataCell, IsEnglish);

                            strSQlInsert.Append(strDataCell + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) ");
                    }
                    ExecuteSQLQuery(strSQlInsert, ExchangeID);
                }
            }
            catch (System.Exception ex)
            {
                _Logger.LogIt("InsertToSQlFromDatatable _ strSQlTableName ="
                    + strSQlTableName + " _ " + ex.ToString()
                    , GrowthConstant.enumLOGType.ERROR.ToString());
                //   throw;
            }
        }

        public int InsertIFNotExist(string StrTableName, string strColumn,
            string strData)
        {
            string strSele = @"select top 1 ID from  " + StrTableName +
                " where " + strColumn + " =  N'" + strData + "'";

            string strResult = getDataFromSQlExecuteScalar(strSele);

            if (strResult == string.Empty)
            {
                string strInsrt
                    = @"INSERT INTO " + StrTableName + " (" + strColumn + " ) VALUES( N'" + strData + "');";
                ExecuteSQLQuery(strInsrt);

                //Select ID after Insert
                strResult = getDataFromSQlExecuteScalar(strSele);
            }
            return int.Parse(strResult);
        }

        public int InsertIFNotExist(string StrTableName, string strColumn,
    string strData, string strInsrt)
        {
            string strSele = @"select top 1 ID from  " + StrTableName +
                " where " + strColumn + " =  N'" + strData + "'";

            string strResult = getDataFromSQlExecuteScalar(strSele);

            if (strResult == string.Empty)
            {
                //string strInsrt
                //    = @"INSERT INTO " + StrTableName + " (" + strColumn + " ) VALUES( N'" + strData + "');";
                ExecuteSQLQuery(strInsrt);

                //Select ID after Insert
                strResult = getDataFromSQlExecuteScalar(strSele);
            }
            return int.Parse(strResult);
        }

        public void InsertToSQlFromDatatable(DataTable DT, string strSQlTableName)
        {
            try
            {
                //Remove Null rows from datatable
                //<
                List<DataRow> listDRDeleted = DT.AsEnumerable().ToList<DataRow>();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    listDRDeleted = (from DataRow dr in listDRDeleted
                                     where ISNull(dr[i].ToString())
                                     select dr).ToList<DataRow>();
                }
                foreach (DataRow DR in listDRDeleted)
                {
                    DT.Rows.Remove(DR);
                }
                //>

                if ((DT != null))
                {
                    string strDataCell = null;
                    StringBuilder strSQlInsert = new StringBuilder();

                    foreach (DataRow DR in DT.Rows)
                    {

                        strSQlInsert.Append("insert into " + strSQlTableName);
                        strSQlInsert.Append(" ( ");
                        foreach (DataColumn DC in DT.Columns)
                        {
                            //write Column names
                            strSQlInsert.Append(DC.ColumnName + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) values (");


                        //write cells values
                        foreach (DataColumn DC in DT.Columns)
                        {
                            strDataCell = DR[DC].ToString();

                            if (((System.Type)DC.DataType).FullName == "System.DateTime")
                            {
                                strDataCell = DateFromat.AdjustDateToSQL(strDataCell);


                            }

                            else if (((System.Type)DC.DataType).FullName == "System.Int32")
                            {
                                strDataCell = (string)GetStrZeroIfNull(strDataCell);
                            }
                            else
                            {
                                strDataCell = (string)GetStrNull(strDataCell, false);
                            }

                            strSQlInsert.Append(strDataCell + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) ");
                    }
                    ExecuteSQLQuery(strSQlInsert);
                }
            }
            catch (System.Exception ex)
            {
                _Logger.LogIt("InsertToSQlFromDatatable _ strSQlTableName ="
                    + strSQlTableName + " _ " + ex.ToString()
                    , GrowthConstant.enumLOGType.ERROR.ToString());
                //   throw;
            }
        }
        public void InsertToSQlFromDatatableExcuteOneByOne(DataTable DT, string ExchangeID, bool IsEnglish)
        {
            string strSQlTableName = DT.TableName;
            try
            {
                //Remove Null rows from datatable
                //<
                List<DataRow> listDRDeleted = DT.AsEnumerable().ToList<DataRow>();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    listDRDeleted = (from DataRow dr in listDRDeleted
                                     where ISNull(dr[i].ToString())
                                     select dr).ToList<DataRow>();
                }
                foreach (DataRow DR in listDRDeleted)
                {
                    DT.Rows.Remove(DR);
                }
                //>

                if ((DT != null))
                {
                    string strDataCell = null;
                    StringBuilder strSQlInsert;

                    foreach (DataRow DR in DT.Rows)
                    {
                        strSQlInsert = new StringBuilder();

                        strSQlInsert.Append("insert into " + strSQlTableName);
                        strSQlInsert.Append(" ( ");
                        foreach (DataColumn DC in DT.Columns)
                        {
                            //write Column names
                            strSQlInsert.Append(DC.ColumnName + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) values (");


                        //write cells values
                        foreach (DataColumn DC in DT.Columns)
                        {
                            strDataCell = DR[DC].ToString();

                            if (((System.Type)DC.DataType).FullName == "System.DateTime")
                            {
                                strDataCell = DateFromat.AdjustDateToSQL(strDataCell);
                            }

                            if (((System.Type)DC.DataType).FullName == "System.Int32")
                            {
                                strDataCell = (string)GetStrZeroIfNull(strDataCell);
                            }

                            strDataCell = (string)GetStrNull(strDataCell, IsEnglish);

                            strSQlInsert.Append(strDataCell + " , ");
                        }
                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);
                        strSQlInsert.Append(" ) ");

                        ExecuteSQLQuery(strSQlInsert, ExchangeID);
                    }

                }
            }
            catch (System.Exception ex)
            {
                _Logger.LogIt("InsertToSQlFromDatatable _ strSQlTableName ="
                    + strSQlTableName + " _ " + ex.ToString()
                    , GrowthConstant.enumLOGType.ERROR.ToString());
                //   throw;
            }
        }
        public void UpdateToSQlFromDatatable(DataTable DT, string strColumnCondition, bool IsEnglish)
        {

            string strSQlTableName = DT.TableName;
            try
            {   //Remove Null rows from datatable
                //<
                List<DataRow> listDRDeleted = DT.AsEnumerable().ToList<DataRow>();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    listDRDeleted = (from DataRow dr in listDRDeleted
                                     where ISNull(dr[i].ToString())
                                     select dr).ToList<DataRow>();
                }

                foreach (DataRow DR in listDRDeleted)
                {
                    DT.Rows.Remove(DR);
                }
                //>

                if ((DT != null))
                {
                    string strDataCell = null;
                    StringBuilder strSQlInsert = new StringBuilder();

                    foreach (DataRow DR in DT.Rows)
                    {

                        strSQlInsert.Append("Update " + strSQlTableName + " Set ");

                        foreach (DataColumn DC in DT.Columns)
                        {
                            //write Column names
                            strSQlInsert.Append(DC.ColumnName + " =  ");

                            //write cells values
                            strDataCell = DR[DC].ToString();

                            if (((System.Type)DC.DataType).FullName == "System.DateTime")
                            {
                                strDataCell = DateFromat.AdjustDateToSQL(strDataCell);
                            }

                            if (((System.Type)DC.DataType).FullName == "System.Int32")
                            {
                                strDataCell = (string)GetStrZeroIfNull(strDataCell);
                            }

                            strDataCell = (string)GetStrNull(strDataCell, IsEnglish);

                            strSQlInsert.Append(strDataCell + " , ");
                        }

                        strSQlInsert = strSQlInsert.Remove(strSQlInsert.Length - 3, 2);

                        //Add Where Condition
                        strSQlInsert.Append("Where " + strColumnCondition + " = " + DR[strColumnCondition].ToString());


                    }
                    ExecuteSQLQuery(strSQlInsert);
                }
            }
            catch (System.Exception ex)
            {
                _Logger.LogIt("InsertToSQlFromDatatable _ strSQlTableName ="
                    + strSQlTableName + " _ " + ex.ToString()
                    , GrowthConstant.enumLOGType.ERROR.ToString());
                //   throw;
            }
        }
        //Return 0 as int to write at sql File
        private static object GetStrZeroIfNull(object value)
        {
            if (!ISNull(value))
            {
                return value;
            }
            else
            {
                return "0";
            }
        }



        //Return NULL as string to write at sql File
        private static object GetStrNull(object value, bool IsEnglish)
        {
            if (!ISNull(value))
            {
                if (IsEnglish)
                    return "'" + value.ToString().Trim().TrimEnd().TrimStart() + "'";
                else
                    return " N'" + value.ToString().Trim() + "'";
            }
            else
            {
                return "NULL";
            }
        }

        private static bool ISNull(object value)
        {
            if (value == null || (object.ReferenceEquals(value, System.DBNull.Value))
                || string.IsNullOrEmpty(value.ToString().Trim()) || value.ToString().Trim().Length < 1
                || value.ToString() == "-"
                )
            {
                return true;
            }

            return false;
        }





        public DataTable CompareDataTables(DataTable first, DataTable second)
        {
            first.TableName = "FirstTable";
            second.TableName = "SecondTable";

            //Create Empty Table
            DataTable table = new DataTable("Difference");

            try
            {
                //Must use a Dataset to make use of a DataRelation object
                using (DataSet ds = new DataSet())
                {
                    //Add tables
                    ds.Tables.AddRange(new DataTable[] { first.Copy(), second.Copy() });

                    //Get Columns for DataRelation
                    DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];

                    for (int i = 0; i < firstcolumns.Length; i++)
                    {
                        firstcolumns[i] = ds.Tables[0].Columns[i];
                    }

                    DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];

                    for (int i = 0; i < secondcolumns.Length; i++)
                    {
                        secondcolumns[i] = ds.Tables[1].Columns[i];
                    }

                    //Create DataRelation
                    DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);

                    ds.Relations.Add(r);

                    //Create columns for return table
                    for (int i = 0; i < first.Columns.Count; i++)
                    {
                        table.Columns.Add(first.Columns[i].ColumnName, first.Columns[i].DataType);
                    }

                    //If First Row not in Second, Add to return table.
                    table.BeginLoadData();

                    foreach (DataRow parentrow in ds.Tables[0].Rows)
                    {
                        DataRow[] childrows = parentrow.GetChildRows(r);
                        if (childrows == null || childrows.Length == 0)
                            table.LoadDataRow(parentrow.ItemArray, true);
                    }

                    table.EndLoadData();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }
    }
}

