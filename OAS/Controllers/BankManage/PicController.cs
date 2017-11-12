using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DBFactoryEntity;
using OAS.Models;

namespace OAS.Controllers.BankManage
{
    public class PicController : BaseController
    {
        public ActionResult Index()
        {
            ModuleNo = "ImageManage";
            ViewBag.ToolBar = BuildPowerButtons();
            return View();
        }

        [HttpPost]
        public JsonResult Folder()
        {
            JsonTree jsontree = new JsonTree()
            {
                id = "imageinfo",
                text = "图片信息"
            };
            string imagepath = Server.MapPath("~/Resource/BankImages");
            buildJsonTree(imagepath, jsontree, imagepath);
            OperateResultModel orm = new OperateResultModel()
            {
                rows = jsontree.children,
                success = true,
                total = 1,
                message = ""
            };
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult Files(string imagefile)
        {
            string rootpath = Server.MapPath("~/Resource/BankImages");
            string path = System.IO.Path.Combine(rootpath, Server.UrlDecode(imagefile).TrimStart('\\').TrimStart('/'));
            string[] files = Directory.GetFiles(path);
            List<Tuple<string, string>> filelist = new List<Tuple<string, string>>();
            foreach (string f in files)
            {
                Tuple<string, string> temp = new Tuple<string, string>(f.Replace(rootpath, ""), System.IO.Path.GetFileName(f));
                filelist.Add(temp);
            }
            OperateResultModel orm = new OperateResultModel()
            {
                rows = filelist,
                success = true
            };
            return JsonResultHelper.ConvertToJsonResult(orm);
        }

        [HttpPost]
        public JsonResult UploadFiles()
        {
            try
            {
                return Json(new { });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult InsertFolder(string level, string oldfolder, string folder)
        {
            try
            {
                string imagepath = Server.MapPath("~/Resource/BankImages");
                string oldfolderpath = imagepath;
                if (!string.IsNullOrEmpty(oldfolder))
                {
                    oldfolderpath = Path.Combine(imagepath, Server.UrlDecode(oldfolder).TrimStart('\\').TrimStart('/'));
                }
                string newfolderpath = string.Empty;
                if (string.IsNullOrEmpty(oldfolder))
                {
                    newfolderpath = Path.Combine(oldfolderpath, folder);
                }
                else if (level == "1")
                {
                    newfolderpath = Path.Combine(Path.GetDirectoryName(oldfolderpath), folder);
                }
                else if (level == "2")
                {
                    newfolderpath = Path.Combine(oldfolderpath, folder);
                }
                else
                {
                    throw new Exception("级别异常");
                }
                string path = Server.UrlDecode(newfolderpath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return Json(new
                {
                    success = true,
                    path = newfolderpath.Replace(imagepath, "")
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult UploadFolder(string folder, string newname)
        {
            try
            {
                string imagepath = Server.MapPath("~/Resource/BankImages");
                string oldfolderpath = Path.Combine(imagepath, Server.UrlDecode(folder).TrimStart('\\').TrimStart('/'));
                string newfolderpath = Path.Combine(Path.GetDirectoryName(oldfolderpath), newname);
                Directory.Move(oldfolderpath, newfolderpath);
                return Json(new
                {
                    success = true,
                    path = newfolderpath.Replace(imagepath, "")
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteFolder(string folder)
        {
            try
            {
                string imagepath = Server.MapPath("~/Resource/BankImages");
                string oldfolderpath = Path.Combine(imagepath, Server.UrlDecode(folder).TrimStart('\\').TrimStart('/'));
                Directory.Delete(oldfolderpath);
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message.IndexOf("目录不是空的") > -1 ? "请先删除图片" : ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult UploadImage(string folder)
        {
            try
            {
                string imagepath = Server.MapPath("~/Resource/BankImages");
                string oldfolderpath = Path.Combine(imagepath, Server.UrlDecode(folder).TrimStart('\\').TrimStart('/'));

                oldfolderpath = Path.Combine(oldfolderpath, Path.GetFileName(Request.Files[0].FileName));
                Request.Files[0].SaveAs(oldfolderpath);

                return Json(new
                {
                    success = true,
                    file = oldfolderpath.Replace(imagepath, "")
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteFiles(string filepath)
        {
            try
            {
                string imagepath = Server.MapPath("~/Resource/BankImages");
                imagepath = Path.Combine(imagepath, Server.UrlDecode(filepath).TrimStart('\\').Trim('/'));
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = true,
                    message = ex.Message
                });
            }
        }

        private void buildJsonTree(string curdir, JsonTree treenode, string replacestr)
        {
            string[] dirs = Directory.GetDirectories(curdir);
            foreach (string dir in dirs)
            {
                JsonTree tree = new JsonTree()
                {
                    id = Server.UrlEncode(dir.Replace(replacestr, "")),
                    text = System.IO.Path.GetFileName(dir)
                };
                treenode.children.Add(tree);
                buildJsonTree(dir, tree, replacestr);
            }
        }
    }
}
