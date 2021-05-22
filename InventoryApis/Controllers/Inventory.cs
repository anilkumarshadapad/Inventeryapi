using InventeroyDTO;
using InventoryApis.Filter;
using InventoryApis.Models;
using InventoryDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace InventoryApis.Controllers
{
    [RoutePrefix("api/InventoryController")]
    public class InventoryController : ApiController
    {
        DBL objDBL = new DBL();
        SqlParameter[] Param;
        DataSet Ds = new DataSet();
        string URL = "Inventory", LogMSG = "I/P";

        [HttpPost]
        [ActionName("InventoryInsert")]
        [Route("InventoryInsert")]
        [ValidationFilter]
        public async Task<InventoryInsertResponse> InventoryInsert(InventoryInsertRequest Request)
        {
            InventoryInsertResponse Response = new InventoryInsertResponse();


            try
            {

                Param = new SqlParameter[4];
                Param[0] = new SqlParameter("@Flag", 1);
                Param[1] = new SqlParameter("@Names", Request.Names);
                Param[2] = new SqlParameter("@Description", Request.Description);
                Param[3] = new SqlParameter("@Price", Request.Price);
                Ds = objDBL.GetDataSet(UserApp_Constant.SP_Inventery, Param, UserApp_Constant.SP_Inventery, URL);
                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {
                        Response.Result = true;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                        Response.InventeryID = Ds.Tables[0].Rows[0]["InventeryID"].ToString();
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                objDBL.LOG(ex.Message.ToString(), "InventoryInsert", "");
                Response.Result = false;
                Response.Meassage = "Error: Something went wrong, try again";
            }
            finally
            {
                Ds.Dispose();
            }
            return await Task.FromResult(Response);

        }


        [HttpPut]
        [ActionName("InventoryUpdate")]
        [Route("InventoryUpdate")]
        [ValidationFilter]
        public async Task<InventoryUpdateResponse> InventoryUpdate(InventoryUpdateRequest Request)
        {
            InventoryUpdateResponse Response = new InventoryUpdateResponse();

            try
            {

                Param = new SqlParameter[5];
                Param[0] = new SqlParameter("@Flag", 2);
                Param[1] = new SqlParameter("@Names", Request.Names);
                Param[2] = new SqlParameter("@Description", Request.Description);
                Param[3] = new SqlParameter("@Price", Request.Price);
                Param[4] = new SqlParameter("@InventeryID", Request.InventeryID);
                Ds = objDBL.GetDataSet(UserApp_Constant.SP_Inventery, Param, UserApp_Constant.SP_Inventery, URL);
                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {
                        Response.Result = true;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                        Response.Name = Ds.Tables[0].Rows[0]["Names"].ToString();
                        Response.Description = Ds.Tables[0].Rows[0]["Description"].ToString();
                        Response.Price = Ds.Tables[0].Rows[0]["Price"].ToString();
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                objDBL.LOG(ex.Message.ToString(), "InventoryUpdate", "");
                Response.Result = false;
                Response.Meassage = "Error: Something went wrong, try again";
            }
            finally
            {
                Ds.Dispose();
            }
            return await Task.FromResult(Response);

        }


        [HttpGet]
        [ActionName("Inventorydetails")]
        [Route("Inventorydetails")]
        [ValidationFilter]
        public async Task<InventorydetailsResponse> Inventorydetails()
        {
            InventorydetailsResponse Response = new InventorydetailsResponse();
            try
            {

                Param = new SqlParameter[1];
                Param[0] = new SqlParameter("@Flag", 3);
                Ds = objDBL.GetDataSet(UserApp_Constant.SP_Inventery, Param, UserApp_Constant.SP_Inventery, URL);
                List<InventoryResponse> objList = new List<InventoryResponse>();

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {

                        Response.Result = true;
                        Response.Meassage = "list";

                        objList = Ds.Tables[0].AsEnumerable()
                                 .Select(op => new InventoryResponse
                                 {
                                     InventeryID= Convert.ToInt32(op["InventeryID"]) != 0 ? Convert.ToInt32(op["InventeryID"].ToString()) : 0,
                                     Name = Ds.Tables[0].Rows[0]["Names"].ToString(),
                                     Description = Ds.Tables[0].Rows[0]["Description"].ToString(),
                                     Price = Ds.Tables[0].Rows[0]["Price"].ToString(),


                                 }).ToList();
                        Response.List = objList;



                    }
                    else
                    {
                        Response.Result = false;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                objDBL.LOG(ex.Message.ToString(), "Inventorydetails", "");
                Response.Result = false;
                Response.Meassage = "Error: Something went wrong, try again";
            }
            finally
            {
                Ds.Dispose();
            }
            return await Task.FromResult(Response);

        }



        [HttpGet]
        [ActionName("InventorydetailsUseingFilters")]
        [Route("InventorydetailsUseingFilters")]
        [ValidationFilter]
        public async Task<InventorydetailsResponse> InventorydetailsUseingFilters(InventorydetailsUseingFiltersRequest Request)
        {
            InventorydetailsResponse Response = new InventorydetailsResponse();
            try
            {

                Param = new SqlParameter[4];
                Param[0] = new SqlParameter("@Flag", 4);
                Param[1] = new SqlParameter("@Names", Request.Names);
                Param[2] = new SqlParameter("@Price", Request.Price);
                Param[3] = new SqlParameter("@InventeryID", Request.InventeryID);
                Ds = objDBL.GetDataSet(UserApp_Constant.SP_Inventery, Param, UserApp_Constant.SP_Inventery, URL);
                List<InventoryResponse> objList = new List<InventoryResponse>();

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {

                        Response.Result = true;
                        Response.Meassage = "list";

                        objList = Ds.Tables[0].AsEnumerable()
                                 .Select(op => new InventoryResponse
                                 {
                                     InventeryID = Convert.ToInt32(op["InventeryID"]) != 0 ? Convert.ToInt32(op["InventeryID"].ToString()) : 0,
                                     Name = Ds.Tables[0].Rows[0]["Names"].ToString(),
                                     Description = Ds.Tables[0].Rows[0]["Description"].ToString(),
                                     Price = Ds.Tables[0].Rows[0]["Price"].ToString(),


                                 }).ToList();
                        Response.List = objList;



                    }
                    else
                    {
                        Response.Result = false;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                objDBL.LOG(ex.Message.ToString(), "InventorydetailsUseingFilters", "");
                Response.Result = false;
                Response.Meassage = "Error: Something went wrong, try again";
            }
            finally
            {
                Ds.Dispose();
            }
            return await Task.FromResult(Response);

        }


        [HttpDelete]
        [ActionName("InventorydetailsDeleteById")]
        [Route("InventorydetailsDeleteById")]
        [ValidationFilter]
        public async Task<InventoryInsertResponse> InventorydetailsDeleteById(InventorydetailsDeleteByIdRequest Request)
        {
            InventoryInsertResponse Response = new InventoryInsertResponse();
            try
            {

                Param = new SqlParameter[2];
                Param[0] = new SqlParameter("@Flag", 5);               
                Param[1] = new SqlParameter("@InventeryID", Request.InventeryID);
                Ds = objDBL.GetDataSet(UserApp_Constant.SP_Inventery, Param, UserApp_Constant.SP_Inventery, URL);

                if (Ds.Tables.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["Result"].ToString() == "1")
                    {
                        Response.Result = true;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                        Response.InventeryID = Ds.Tables[0].Rows[0]["InventeryID"].ToString();
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Meassage = Ds.Tables[0].Rows[0]["Meassage"].ToString();
                    }
                }


            }

            catch (Exception ex)
            {
                objDBL.LOG(ex.Message.ToString(), "InventorydetailsDeleteById", "");
                Response.Result = false;
                Response.Meassage = "Error: Something went wrong, try again";
            }
            finally
            {
                Ds.Dispose();
            }
            return await Task.FromResult(Response);

        }






    }
}
