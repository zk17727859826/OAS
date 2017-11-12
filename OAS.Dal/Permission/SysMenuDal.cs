using DBFactoryEntity;
using OAS.Model.Permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Permission
{
    public class SysMenuDal : BaseDal<sysmenu>
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public sysmenu Query(string menuno)
        {
            sysmenu menu = new sysmenu() { menuno = menuno };
            menu = QueryEntity(menu);
            return menu;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <returns></returns>
        public OperateResultModel Query()
        {
            OperateResultModel orm = Query(null, null, null);
            if (orm.rows != null)
            {
                List<sysmenu> menus = ModelHelper.ToModel<List<sysmenu>>((DataTable)orm.rows);
                var topmenus = menus.Where(p => p.parentno == "0");
                foreach (sysmenu menu in topmenus)
                {
                    BuildMenuTree(menus, menu);
                }
                orm.rows = topmenus;
            }
            return orm;
        }

        /// <summary>
        /// 创建菜单树
        /// </summary>
        /// <param name="menus">所有菜单信息</param>
        /// <param name="curmenu">当前菜单信息</param>
        private void BuildMenuTree(List<sysmenu> menus, sysmenu curmenu)
        {
            var submenus = menus.Where(p => p.parentno == curmenu.menuno);
            foreach (sysmenu menu in submenus)
            {
                BuildMenuTree(menus, menu);
                curmenu.children = submenus.ToList();
            }
        }

        /// <summary>
        /// 插入菜单信息
        /// </summary>
        /// <param name="menu">菜单实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysmenu menu)
        {
            return dbhelper.Insert(menu);
        }

        /// <summary>
        /// 更新菜间信息
        /// </summary>
        /// <param name="menu">菜单实体</param>
        /// <returns></returns>
        public OperateResultModel Update(sysmenu menu)
        {
            return dbhelper.Update(menu);
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="menuno">菜单编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string menuno)
        {
            sysmenu menu = new sysmenu() { menuno = menuno };
            List<DBMemberEntity> entities=new List<DBMemberEntity>();
            entities.AddMember("parentno", menuno);
            object obj = dbhelper.QueryScalar("select count(0) from sysmenu t where t.parentno = @parentno", entities);
            if (Convert.ToInt32(obj) == 0)
            {
                return dbhelper.Delete(menu);
            }
            else
            {
                return new OperateResultModel()
                {
                    success = false,
                    message = "请先删除子菜单"
                };
            }
        }
    }
}
