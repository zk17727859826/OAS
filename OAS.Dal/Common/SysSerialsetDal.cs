using DBFactoryEntity;
using OAS.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg.Helper;

namespace OAS.Dal.Common
{
    public class SysSerialsetDal : BaseDal<sysserialset>
    {
        /// <summary>
        /// 获得序列号
        /// </summary>
        /// <param name="serialno">序列编号</param>
        /// <param name="specialtext">特殊字符</param>
        /// <returns></returns>
        public string GetSerialNo(string serialno, string specialtext)
        {
            sysserialset serialinfo = new sysserialset()
            {
                serialno = serialno
            };
            serialinfo = QueryEntity(serialinfo);
            if (serialinfo == null)
            {
                throw new Exception("输入的序列编号不存在");
            }

            string newno = string.Empty;
            if (!string.IsNullOrEmpty(serialinfo.prefix))
            {
                newno += serialinfo.prefix;
            }
            if (!string.IsNullOrEmpty(serialinfo.preflag))
            {
                newno += serialinfo.preflag;
            }
            if (!string.IsNullOrEmpty(specialtext))
            {
                newno += specialtext;
            }            
            if (!string.IsNullOrEmpty(serialinfo.midfix))
            {
                newno += serialinfo.midfix;
            }
            if (!string.IsNullOrEmpty(serialinfo.midflag))
            {
                newno += serialinfo.midflag;
            }            
            if (!string.IsNullOrEmpty(serialinfo.lastfix))
            {
                newno += serialinfo.lastfix;
            }
            if (!string.IsNullOrEmpty(serialinfo.lastflag))
            {
                newno += serialinfo.lastflag;
            }
            if (serialinfo.yearnum != null)
            {
                if (serialinfo.yearnum == 2)
                {
                    newno += DateTime.Now.ToString("yy");
                }
                else if (serialinfo.yearnum == 4)
                {
                    newno += DateTime.Now.ToString("yyyy");
                }
            }
            if (serialinfo.monthnum != null)
            {
                newno += DateTime.Now.ToString("MM");
            }
            if (serialinfo.daynum != null)
            {
                newno += DateTime.Now.ToString("dd");
            }

            sysserialtb tb = new sysserialtb()
            {
                serialno = serialno,
                serialfix = newno
            };

            SysSerialtbDal tbdal = new SysSerialtbDal();
            tb = tbdal.QueryEntity(tb);

            using (dbhelper)
            {
                try
                {
                    bool issuccess = false;
                    decimal curnum = 1;
                    if (tb != null)
                    {
                        tb.serialfix = newno;
                        tb.serialno = serialno;
                        curnum = tb.currentnumber.Value + 1;
                        tb.currentnumber = curnum;

                        issuccess = tbdal.UpdateSerialTb(dbhelper, tb);
                    }
                    else
                    {
                        tb = new sysserialtb()
                        {
                            serialfix = newno,
                            currentnumber = curnum,
                            serialno = serialno
                        };
                        issuccess = tbdal.InsertSerialTb(dbhelper, tb);
                    }
                    if (!issuccess)
                    {
                        throw new Exception("生成编号失败");
                    }
                    newno += curnum.ToString().PadLeft(serialinfo.serialnum.Value, '0');
                    dbhelper.Transaction().Commit();
                }
                catch (Exception)
                {
                    dbhelper.Transaction().Rollback();
                    throw;
                }
            }

            return newno;
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="serialno">角色编号</param>
        /// <returns></returns>
        public sysserialset Query(string serialno)
        {
            sysserialset serialmodel = new sysserialset() { serialno = serialno };
            serialmodel = QueryEntity(serialmodel);
            return serialmodel;
        }

        /// <summary>
        /// 查询数据信息
        /// </summary>
        /// <param name="serialno">编号代码</param>
        /// <param name="serialname">编号名称</param>
        /// <returns></returns>
        public OperateResultModel Query(string serialno,string serialname)
        {
            List<DBMemberEntity> entities = new List<DBMemberEntity>();
            if (!string.IsNullOrEmpty(serialno))
            {
                entities.AddMember("serialno", serialno, QueryTypeEnum.fruzz);
            }
            if (!string.IsNullOrEmpty(serialname))
            {
                entities.AddMember("serialname", serialname, QueryTypeEnum.fruzz);
            }
            OperateResultModel orm = Query(entities, null, null);
            if (orm.rows != null)
            {
                orm.rows = ModelHelper.ToModel<List<sysserialset>>((DataTable)orm.rows);
            }
            return orm;
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="serialmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Insert(sysserialset serialmodel)
        {
            return dbhelper.Insert(serialmodel);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="serialmodel">实体</param>
        /// <returns></returns>
        public OperateResultModel Update(sysserialset serialmodel)
        {
            return dbhelper.Update(serialmodel);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="serialno">编号</param>
        /// <returns></returns>
        public OperateResultModel Delete(string serialno)
        {
            sysserialset role = new sysserialset() { serialno = serialno };
            return dbhelper.Delete(role);
        }
    }
}
