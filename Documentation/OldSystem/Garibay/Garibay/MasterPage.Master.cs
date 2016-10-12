using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;

namespace Garibay
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfoUser.Text = (String)this.Session["usuario"];
            
            if (!IsPostBack)
            {
                if (this.IsSistemBanco)
                {
                    this.Image1.ImageUrl = "/imagenes/LogoMargaritasSmall.jpg";
                }
                Session["Tema"] = this.DDLTEMA.SelectedItem.Text;
                //Session["Menu"] = this.cmbMenu.SelectedItem.Text;
                //JSUtils.OpenNewWindowOnClick(ref this.btnAddSituacion, , "Nueva Situacion", true);

                String strRedirect = "frmSituacionAddUpdate.aspx";
                String ventanatitle = "Reportar Situacion";
                String sOnClick = "javascript:url('";
                sOnClick += strRedirect + "', '";
                sOnClick += ventanatitle;
                sOnClick += "',100,100,600,600);";
                sOnClick += "return false;";
                this.btnAddSituacion.Attributes.Add("onClick", sOnClick);
                this.verificaMenu();
            }
            this.Label1.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.compruebasecurityLevel();
            

        }
        public bool IsSistemBanco
        {
            get
            {
                return this.Session["SISTEMABANCO"] != null && ((bool)this.Session["SISTEMABANCO"]) ? true : false;
            }
        }
        protected void verificaMenu()
        {
            if (!this.IsSistemBanco)
            {
                this.pnlMenuBanco.Visible = false;
                this.Menu2.Visible = false;
                this.Menu1.Visible = false;
                this.pnlDivMenu3.Visible = false;
                this.pnlSideMenu.Visible = false;
                this.pnlSuperFish.Visible = false;

                if (this.Session["Menu"] == null)
                {
                    this.Session["Menu"] = this.cmbMenu.SelectedValue;
                }
                if (this.Session["Menu"].ToString() == "MenuSS")
                {
                    this.pnlSuperFish.Visible = true;
                }
                else
                {
                    this.pnlSideMenu.Visible = true;
                    if (this.Session["Menu"].ToString() == "Menu 1")
                    {
                        this.pnlDivMenu3.Visible = true;
                        this.cmbMenu.SelectedValue = "Menu 1";
                    }
                    else
                    {
                        if (this.Session["Menu"].ToString() == "Menu 2")
                        {
                            this.Menu1.Visible = true;
                            this.cmbMenu.SelectedValue = "Menu 2";
                        }
                        else
                        {
                            this.Menu2.Visible = true;
                            this.cmbMenu.SelectedValue = "Menu 3";
                        }
                    }
                }
            }
            else
            {
                this.Menu2.Visible = false;
                this.Menu1.Visible = false;
                this.pnlDivMenu3.Visible = false;
                this.pnlMenuBanco.Visible = true;
            }
            
        }

        protected void compruebasecurityLevel()
        {   
            if(this.Session["SECURITYID"]!=null){
                ArrayList paginassinacceso = new ArrayList();
                paginassinacceso.Add("Usuarios"); paginassinacceso.Add("Ciclos");
                paginassinacceso.Add("Cuentas de banco"); paginassinacceso.Add("Creditos Financieros");
                paginassinacceso.Add("Conciliación"); paginassinacceso.Add("Agregar Movimiento");
                paginassinacceso.Add("Agregar Productor"); 
                paginassinacceso.Add("Edo. de cuenta"); paginassinacceso.Add("Agregar predio");
                paginassinacceso.Add("Existencias de productos"); paginassinacceso.Add("Agregar entrada");
                paginassinacceso.Add("Agregar salida"); paginassinacceso.Add("Agregar nota de compra");
                paginassinacceso.Add("Agregar nota de venta"); paginassinacceso.Add("Agregar crédito");
                paginassinacceso.Add("Agregar solicitud"); paginassinacceso.Add("Aceptar/Negar solicitud");
                paginassinacceso.Add("Agregar Boletas"); paginassinacceso.Add("Cargar boletas");
                paginassinacceso.Add("Realizar Liquidación"); paginassinacceso.Add("Asignar anticipo a liquidación");
                paginassinacceso.Add("Nueva Factura"); paginassinacceso.Add("Conceptos (caja chica)");
                paginassinacceso.Add("Catalogo de cuentas y Subcatálogos de cuenta"); paginassinacceso.Add("Configuracion Global");
                paginassinacceso.Add("Administración"); paginassinacceso.Add("Formatos");
                paginassinacceso.Add("Change Log"); paginassinacceso.Add("Publicaciones");
                paginassinacceso.Add("Ganado - Proveedores"); //paginassinacceso.Add("Conceptos (caja chica)");
 


               // paginassinacceso.Add("Cuentas de Banco"); paginassinacceso.Add("Creditos Financieros");

                if (int.Parse(this.Session["SECURITYID"].ToString())==4)
                {
                   
                    int menu = 0, submenu = 0, subsubmenu=0;
                   
                    
                      for(menu=0; menu<Menu1.Items.Count; menu++){
                          for (int i = 0; i < paginassinacceso.Count; i++ )
                          {
                              if (Menu1.Items[menu].Text.IndexOf(paginassinacceso[i].ToString()) > -1)
                              {
                                  Menu1.Items[menu].Enabled = false;

                              }
                          }

                          for(submenu=0 ; submenu<Menu1.Items[menu].ChildItems.Count; submenu++){
                              for (int i = 0; i < paginassinacceso.Count; i++)
                              {
                                  if (Menu1.Items[menu].ChildItems[submenu].Text.IndexOf(paginassinacceso[i].ToString()) > -1)
                                  {
                                      Menu1.Items[menu].ChildItems[submenu].Enabled = false;

                                  }
                              }
                              for (subsubmenu = 0; subsubmenu < Menu1.Items[menu].ChildItems[submenu].ChildItems.Count; subsubmenu++)
                              {
                                  for (int i = 0; i < paginassinacceso.Count; i++)
                                  {
                                      if (Menu1.Items[menu].ChildItems[submenu].ChildItems[subsubmenu].Text.IndexOf(paginassinacceso[i].ToString()) > -1)
                                      {
                                          Menu1.Items[menu].ChildItems[submenu].ChildItems[subsubmenu].Enabled = false;

                                      }
                                  }


                              }


                          }

                          
                      }
                      

//                     this.Menu1.Items[1].Text = false;
//                     this.Menu1.Items[2].Selectable = false;
//                     
//                     this.Menu1.Items[3].ChildItems[6].Selectable = false;
//                     this.Menu1.Items[4].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[5].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[5].ChildItems[2].Selectable = false;
//                     this.Menu1.Items[6].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[9].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[10].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[11].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[12].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[13].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[14].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[14].ChildItems[2].Selectable = false;
//                     this.Menu1.Items[15].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[15].ChildItems[2].Selectable = false;
//                     this.Menu1.Items[16].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[16].ChildItems[2].Selectable = false;
//                     this.Menu1.Items[17].ChildItems[1].Selectable = false;
//                     this.Menu1.Items[18].ChildItems[0].Selectable = false;
//                     this.Menu1.Items[18].ChildItems[3].Selectable = false;
//                     this.Menu1.Items[19].Selectable = false;
//                     this.Menu1.Items[20].Selectable = false;
//                     this.Menu1.Items[20].ChildItems[0].Selectable = false;
                }
            }

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
            this.Session.Clear();
            Server.Transfer("~/logout.aspx");
          
            
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void lblApplyTheme_Click(object sender, EventArgs e)
        {
            Session["Tema"] = this.DDLTEMA.SelectedItem.Text;
            Response.Redirect(Request.Url.ToString()); 
            
          
            
        }

        protected void btnException_Click(object sender, EventArgs e)
        {
            
        }

        protected void lnkMenu_Click(object sender, EventArgs e)
        {
            Session["Menu"] = this.cmbMenu.SelectedValue;
            Response.Redirect(Request.Url.ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmBusquedaGeneral.aspx" + Utils.GetEncriptedQueryString("Search=" + this.txtSearch.Text.ToUpper()));
        }

        
       

       
    }
}
