using AtomicCore.DbProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AtomicCore.Integration.MssqlDbProviderUnitTest
{
    [TestClass]
    public class MssqlDbProviderTests
    {
        #region Constructors

        public MssqlDbProviderTests()
        {
            AtomicKernel.Initialize();
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// 测试读取配置文件
        /// </summary>
        [TestMethod]
        public void TesConfigurationJsonManagerMethod()
        {
            string key = ConfigurationJsonManager.AppSettings["symmetryKey"];

            ConnectionStringJsonSettings confs = ConfigurationJsonManager.ConnectionStrings["DLYS_MNKS"];

            Assert.IsTrue(null != confs);
        }

        /// <summary>
        /// 新增单条数据
        /// </summary>
        [TestMethod]
        public void TestCreateMethod()
        {
            DbSingleRecord<Topic_QQS> insertResult = BizDbRepository.Topic_QQS.Insert(new Topic_QQS()
            {
                qq = "10001",
                text = "test",
                isdel = true
            });

            Assert.IsTrue(insertResult.IsAvailable());
        }

        /// <summary>
        /// 更新指定部分字段
        /// </summary>
        [TestMethod]
        public void TestUpdateSinglePartial()
        {
            string qq = null;

            DbNonRecord upResult = BizDbRepository.Topic_QQS.Update(d =>
                d.qq == qq,
            up => new Topic_QQS()
            {
                text = "update partial success"
            });

            Assert.IsTrue(upResult.IsAvailable());
        }

        /// <summary>
        /// 更新模型的所有字段
        /// </summary>
        [TestMethod]
        public void TestUpdateSingleFull()
        {
            DbSingleRecord<Topic_QQS> getResult = BizDbRepository.Topic_QQS.Fetch(o => o.Where(d => d.qq == "10001").OrderByDescending(d => d.id));
            if (!getResult.IsAvailable())
            {
                Assert.Fail(getResult.Errors.First());
                return;
            }
            if (null == getResult.Record)
            {
                Assert.Fail("数据不存在,无法进行如下测试");
                return;
            }

            Topic_QQS model = getResult.Record;
            model.text = "update full success";

            DbNonRecord upResult = BizDbRepository.Topic_QQS.Update(d => d.qq == "10001", model);

            Assert.IsTrue(upResult.IsAvailable());
        }

        /// <summary>
        /// 批量更新操作
        /// </summary>
        [TestMethod]
        public void TestUpdateBatch()
        {
            DbCollectionRecord<Topic_QQS> listResult = BizDbRepository.Topic_QQS.FetchList(o => o.OrderBy(d => d.id));
            if (!listResult.IsAvailable())
            {
                Assert.IsTrue(false);
                return;
            }
            if (null == listResult.Record)
            {
                Assert.IsTrue(false);
                return;
            }

            listResult.Record.ForEach(e => e.isdel = false);

            DbNonRecord result = BizDbRepository.Topic_QQS.UpdateBatch(listResult.Record);

            Assert.IsTrue(result.IsAvailable());
        }

        /// <summary>
        /// 批量更新任务
        /// </summary>
        [TestMethod]
        public void TestUpdateTask()
        {
            List<DbUpdateTaskData<Topic_QQS>> taskList = new List<DbUpdateTaskData<Topic_QQS>>();
            taskList.Add(new DbUpdateTaskData<Topic_QQS>()
            {
                WhereExp = d => d.qq == "171331668",
                UpdatePropertys = up => new Topic_QQS()
                {
                    text = "task update 171331668",
                    isdel = true
                }
            });
            taskList.Add(new DbUpdateTaskData<Topic_QQS>()
            {
                WhereExp = d => d.qq == "442543251",
                UpdatePropertys = up => new Topic_QQS()
                {
                    text = "task update 442543251",
                    isdel = true
                }
            });

            DbNonRecord result = BizDbRepository.Topic_QQS.UpdateTask(taskList, true);

            Assert.IsTrue(result.IsAvailable());
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            DbNonRecord delResult = BizDbRepository.Topic_QQS.Delete(d => d.qq == "10001");
            if (!delResult.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void FetchTest()
        {
            DbSingleRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.Fetch(o => o
                .Where(d => d.isdel)
                .OrderBy(d => d.qq)
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void FetchListTest()
        {
            DbCollectionRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.FetchList(o => o
                .Pager(1, int.MaxValue)
                .Where(d => !d.isdel && d.id > 0)
                .OrderBy(d => d.id)
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void CalculateTest()
        {
            DbCalculateRecord result = BizDbRepository.Topic_QQS.Calculate(o => o
                .Select(d => new Topic_QQS() { id = d.id })
                .Sum(d => d.id, "total")
                .Where(d => d.id > 0)
                .GroupBy(d => d.id)
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SqlExt_Contains()
        {
            DbCollectionRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.FetchList(o => o
                .Where(d =>
                    d.id > 0 &&
                    d.text.Contains("171331668")
                )
                .OrderBy(d => d.id)
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SqlExt_DbIn()
        {
            string caseins = "1,2,3,4,5,6,7,8,9,10";
            DbCollectionRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.FetchList(o => o
                .Where(d => 
                    d.id > 0 && 
                    d.SqlIn(d.id, caseins)
                )
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SqlExt_DbNotIn()
        {
            string caseins = "1,2";
            DbCollectionRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.FetchList(o => o
                .Where(d => 
                    d.id > 0 && 
                    d.SqlNotIn(d.id, caseins)
                )
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SqlExt_DbSubQuery()
        {
            DbCollectionRecord<Topic_QQS> result = BizDbRepository.Topic_QQS.FetchList(o => o
                .Where(d => 
                    !d.isdel && 
                    d.id > 0 && 
                    d.SqlSubQuery<Topic_QQS_Ext>(child => 
                        child.qq == d.qq && 
                        child.name == d.text, 
                        child => 
                            child.sex > 0 && 
                            child.name != "" && 
                            d.id > 0
                        , true
                    )
                )
            );
            if (!result.IsAvailable())
                Assert.Fail();
            else
                Assert.IsTrue(true);
        }

        #endregion
    }
}
