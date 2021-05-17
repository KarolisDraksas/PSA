using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PSA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Profile> profiles = new List<Profile>();
        List<following> followings = new List<following>();
        List<Profile> followedProfiles = new List<Profile>();
        List<following> followed = new List<following>();
        List<Profile> ll = new List<Profile>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = PSA.Properties.Resources.ConnectionString;
        }

        private void FetchData()
        {
            if (profiles.Count > 0)
            {
                profiles.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Id],[Email],[FirstName],[LastName] FROM [PSA].[dbo].[AspNetUsers]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    profiles.Add(new Profile()
                    {
                        ID = dr["Id"].ToString()
                    ,
                        Email = dr["Email"].ToString()
                    ,
                        FirstName = dr["FirstName"].ToString()
                    ,
                        LastName = dr["LastName"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /*private void AddFollowers(string followeddUserID)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "INSERT INTO [PSA].[dbo].[Followings] ([userID], [followedUserID]) VALUES ('id1', @followedUserID) ";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/
        /*public ActionResult TimeLine(Microsoft.AspNetCore.Http.FormCollection form) { 
            try
            {
                var dest = form["AddFollowers"];
                con.Open();
                com.Connection = con;
                com.CommandText = "INSERT INTO [PSA].[dbo].[Followings] ([userID], [followedUserID]) VALUES ('id1', @followedUserID) ";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Profile()
        {
            FetchData();
            return View(profiles);
        }

        public IActionResult ViewUsers()
        {
            FetchData();
            return View(profiles);
        }
        [HttpGet("ViewUsers/{ID}")]
        public IActionResult ViewUsers(string ID)
        {
            if (followings.Count > 0)
            {
                followings.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [userID],[followedUserID] FROM [PSA].[dbo].[Followings2]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    followings.Add(new following()
                    {
                        UserID = dr["userID"].ToString()
                    ,
                        followedUserID = dr["followedUserID"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            try
            {     
                string[] pp = ID.Split();
                string id1 = pp[0];
                string id2 = pp[1];
                bool allow = true;
                foreach(following b in followings)
                {
                    if (b.followedUserID.Trim() == id1)
                    {
                        if (b.UserID.Trim() == id2)
                        {
                            allow = false;
                            break;
                        }
                    }
                }
                if (allow == true)
                {
                    con.Open();
                    com.Connection = con;
                    com.Parameters.AddWithValue("@id1", id1);
                    com.Parameters.AddWithValue("@id2", id2);
                    com.CommandText = "INSERT INTO [PSA].[dbo].[Followings2] ([userID], [followedUserID]) VALUES (@id2, @id1) ";
                    com.ExecuteNonQuery();
                    con.Close();
                    ViewBag.Message = string.Format("User followed successfully");
                }
                else
                {
                   /* Url.Action("ViewUsers", "Home");*/
                    ViewBag.Message = string.Format("Already following this user");
                   /* return RedirectToAction("ViewUsers");
                    /* Url.Action("ViewUsers", "Home");*/
                    /*return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                    /*ScriptManager
                    Page.ClientScript.
                    Response.BodyWriter.WriteAsync("<script>alert('Inserted..');window.location = 'newpage.aspx';</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insert is successfull')", true);*/
                }
                /*con.Open();
                com.Connection = con;
                com.Parameters.AddWithValue("@id1", id1);
                com.Parameters.AddWithValue("@id2", id2);
                com.CommandText = "INSERT INTO [PSA].[dbo].[Followings] ([userID], [followedUserID]) VALUES (@id1, @id2) ";
                com.ExecuteNonQuery();
                con.Close();*/
            }
            catch (Exception ex)
            {

                throw ex;
            }
            /*Url.Action("ViewUsers", "Home");*/
           
           FetchData();
           return View(profiles);
           
        }

        private void FetchFollowingData()
        {
            if (followedProfiles.Count > 0)
            {
                followedProfiles.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Id],[Email],[FirstName],[LastName] FROM [PSA].[dbo].[AspNetUsers]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    followedProfiles.Add(new Profile()
                    {
                        ID = dr["Id"].ToString()
                    ,
                        Email = dr["Email"].ToString()
                    ,
                        FirstName = dr["FirstName"].ToString()
                    ,
                        LastName = dr["LastName"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }



            if (followed.Count > 0)
            {
                followed.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [userID],[followedUserID] FROM [PSA].[dbo].[Followings2]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    followed.Add(new following()
                    {
                        UserID = dr["userID"].ToString()
                    ,
                        followedUserID = dr["followedUserID"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (ll.Count > 0)
            {
                ll.Clear();
            }
            foreach (Profile p in followedProfiles)
            {
                foreach (following f in followed)
                {
                    if (f.followedUserID.Trim() == p.ID.Trim())
                    {
                        ll.Add(p);
                        break;
                    }
                }
            }
        }
       

        public IActionResult ViewFollowedUsers()
        {
            FetchFollowingData();
            return View(ll);
        }

        [HttpGet("ViewFollowedUsers/{ID}")]
        public IActionResult ViewFollowedUsers(string ID)
        {
            try
            {
             string[] pp = ID.Split();
             string id1 = pp[0];
             string id2 = pp[1];
              con.Open();
              com.Connection = con;
              com.Parameters.AddWithValue("@ID1", id1);
              com.Parameters.AddWithValue("@ID2", id2);
              com.CommandText = "DELETE FROM [PSA].[dbo].[Followings2] WHERE [followedUserID] = @ID1 AND [userID] = @ID2 ";
              com.ExecuteNonQuery();
              con.Close();
              ViewBag.Message = string.Format("User unfollowed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Message = string.Format("User unfollowed");

            FetchFollowingData();
            return View(ll);
        }
        /*public IActionResult ViewUsers2()
        {
            try
            {
                /*string id1 = foll.UserID;
                string id2 = foll.followedUserID;*/
        /*        string id1 = "aa";
                string id2 = "bb";
                con.Open();
                com.Connection = con;
                com.CommandText = "INSERT INTO [PSA].[dbo].[Followings] ([userID], [followedUserID]) VALUES (@id1, @id2) ";
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            FetchData();
            return View(profiles);
        }*/
        /* public IActionResult AddFollower()
         {
             FetchData();
             AddFollowers(followeddUserID);
             return View(profiles);
         }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
