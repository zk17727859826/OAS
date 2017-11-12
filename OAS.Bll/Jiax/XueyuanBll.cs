using DBFactoryEntity;
using OAS.Dal.Jiax;
using OAS.Model.Jiax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Bll
{
    public partial class JiaxInfo
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public tbl_xueyuan QueryXueyuan(string xueyh)
        {
            TblXueyuanDal dal = new TblXueyuanDal();
            return dal.Query(xueyh);
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="xingming">姓名</param>
        /// <param name="shenfhm">身份号码</param>
        /// <param name="pm">分页信息</param>
        /// <returns></returns>
        public OperateResultModel QueryXueyuans(string xueyh, string xingming, string shenfhm, PaginModel pm)
        {
            TblXueyuanDal dal = new TblXueyuanDal();
            return dal.Query(xueyh, xingming, shenfhm, pm);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel InsertXueyuan(tbl_xueyuan model)
        {
            TblXueyuanDal dal = new TblXueyuanDal();
            return dal.Insert(model);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OperateResultModel UpdateXueyuan(tbl_xueyuan model)
        {
            TblXueyuanDal dal = new TblXueyuanDal();
            return dal.Update(model);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <returns></returns>
        public OperateResultModel DeleteXueyuan(string xueyh)
        {
            TblXueyuanDal dal = new TblXueyuanDal();
            return dal.Delete(xueyh);
        }

        /// <summary>
        /// 学员是否
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="password">密码</param>
        /// <param name="jiaxid">驾校ID</param>
        /// <returns></returns>
        public bool IsValid(string xueyh, string password, string jiaxid)
        {
            tbl_xueyuan xueyuan = QueryXueyuan(xueyh);
            if (xueyuan == null)
            {
                return false;
            }

            if (jiaxid != xueyuan.jiaxid)
            {
                throw new Exception("你不是当前驾校的学员");
            }

            return xueyuan.password == password && xueyuan.jiaxid == jiaxid;
        }

        /// <summary>
        /// 更新学员密码
        /// </summary>
        /// <param name="xueyh">学员号</param>
        /// <param name="oldpassword">旧密码</param>
        /// <param name="newpassword">新密码</param>
        /// <returns></returns>
        public OperateResultModel UpdatePassowrd(string xueyh, string oldpassword, string newpassword, string editer)
        {
            OperateResultModel orm = new OperateResultModel();
            tbl_xueyuan xueyuan = QueryXueyuan(xueyh);
            if (xueyuan == null)
            {
                orm.success = false;
                orm.message = "学员号不存在";
            }
            else
            {
                if (xueyuan.password == oldpassword)
                {
                    tbl_xueyuan newmodel = new tbl_xueyuan()
                    {
                        xueyh = xueyh,
                        password = newpassword,
                        editdate = DateTime.Now,
                        editer = editer
                    };
                    orm.success = true;
                }
                else
                {
                    orm.success = false;
                    orm.message = "旧密码不正确";
                }
            }
            return orm;
        }
    }
}
