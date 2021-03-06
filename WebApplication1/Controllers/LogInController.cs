﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Actions;
using WebApplication1.Models;
using System.Configuration;

namespace System.Windows.Forms
{
    namespace WebApplication1.Controllers
    {
        public class LogInController : Controller
        {
            // GET: LogIn
            [HttpGet]
            public ActionResult LogIn()
            {
                return View("LogIn");
            }
            [HttpPost]
            public ActionResult LogIn(LoginModel log)
            {
                if (ModelState.IsValid)
                {
                    hackprodb_4Entities db = new hackprodb_4Entities();
                    
                    var login = db.tbl_usuario.Where(p => p.tbl_usuario_correo.Equals(log.User) && p.tbl_usuario_password.Equals(log.Pass));
                  
                    if (login.Count() == 1)
                    {
                        //Session["User"] = "Jose4";
                        return RedirectToAction("Admin", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Correo O Contraseña no Validos");
                    }

                    string correo = "";
                    string nombre = "";
                    string apellido = "";
                    bool isAdmin = false;

                    foreach (var user in db.tbl_usuario.Where(n => n.tbl_usuario_correo.Equals(log.User)))
                    {


                        correo = log.User;
                        nombre = user.tbl_usuario_p_nombre;
                        apellido = user.tbl_usuario_p_apellido;
                        isAdmin = user.tbl_usuario_admin;
                        
                    }

                    if (correo == "" && nombre == "" && apellido == "")
                    {
                        correo = nombre = apellido = "No Valido";
                    }

                    Diagnostics.Debug.WriteLine("Correo " + correo);
                    Diagnostics.Debug.WriteLine("Nombre " + nombre);
                    Diagnostics.Debug.WriteLine("Apellido " + apellido);
                    Diagnostics.Debug.WriteLine("Is Admin " + isAdmin);

                    ViewBag.correo = correo;
                    ViewBag.nombre = nombre + " " + apellido;
                    ViewBag.isAdmin = isAdmin;
                }
                return View();
            }

            [HttpGet]
            public ActionResult Register()
            {
                return View("Register");
            }

            [HttpPost]
            public ActionResult Register(UserModel user)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("Es Valido");
                    DateTime Fecha = DateTime.Now;
                    //ConnectUpdate udb = new ConnectUpdate();
                    hackprodb_4Entities db = new hackprodb_4Entities();
                    var check = db.tbl_usuario.Where(p => p.tbl_usuario_correo.Equals(user.Correo));
                    if (check.Any() == false)
                    {
                        tbl_usuario Usuario = new tbl_usuario();
                        Usuario.tbl_usuario_correo = user.Correo;
                        Usuario.tbl_usuario_username = user.UserName;
                        Usuario.tbl_usuario_password = user.Password;
                        Usuario.tbl_usuario_p_nombre = user.PNombre;
                        Usuario.tbl_usuario_s_nombre = user.SNombre;
                        Usuario.tbl_usuario_p_apellido = user.PApellido;
                        Usuario.tbl_usuario_s_apellido = user.SApellido;
                        Usuario.tbl_usuario_activo = true;
                        Usuario.tbl_usuario_celular = user.Celular;
                        Usuario.tbl_usuario_fecha_nac = user.FechaN;
                        Usuario.tbl_usuario_genero = user.Genero;
                        Usuario.tbl_usuario_ocupacion = user.Ocupacion;
                        Usuario.tbl_usuario_fecha_crea = Fecha;
                        Usuario.tbl_usuario_admin = false;
                        db.tbl_usuario.Add(Usuario);
                        db.SaveChanges();
                        return View("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorC", "Este Correo ya Existe");
                    }
                }
                
                System.Diagnostics.Debug.WriteLine("No Es Valido");
                return View();
            }

            }
    }
}