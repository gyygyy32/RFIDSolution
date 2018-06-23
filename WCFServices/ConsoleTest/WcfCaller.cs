using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RFIDService.ClientData;
using RFIDService.Clients;
using System.ServiceModel;

namespace ConsoleTest
{
    public static class WcfCaller
    {
        public static void querySerialInfo(Action<ModuleInfo, Exception> action, string[] prms)
        {
            RFIDServiceClient client = null;

            try
            {
                client = new RFIDServiceClient();
                action(client.getModuleInfo(prms), null);

            }
            #region excption handling
            catch (CommunicationException e)
            {
                action(null, new Exception(string.Format("服务端程序发生内部错误，错误原因：{0}", e.Message)));
            }
            catch (TimeoutException)
            {
                action(null, new Exception("连接超时，请检查网络或者服务器状态！"));
            }
            catch (Exception e)
            {
                action(null, e);
            }
            finally
            {
                ICommunicationObject comObj = ((ICommunicationObject)client);

                if (comObj.State == CommunicationState.Faulted)
                {
                    comObj.Abort();
                }
                else
                {
                    comObj.Close();
                }
            }
            #endregion
        }

    }
}
