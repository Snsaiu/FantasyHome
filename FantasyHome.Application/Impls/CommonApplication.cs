using System;
using FantasyHome.Application.Dto;
using FantasyHome.GTool;
using Newtonsoft.Json;
using RestSharp;

namespace FantasyHome.Application.Impls
{

    public class CommonApplication : ICommonApplication
    {
        public ResultBase<string> TestConnect(TryConnectParamInput input)
        {
            var client = new RestClient($"http://{input.Host}:{input.Port}/api/common/try-test");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.Content=="")
            {
                return new ResultBase<string>() { Succeeded = false };
            }
            return JsonConvert.DeserializeObject<ResultBase<string>>( response.Content);
        }

        public ResultBase<RegistResultOutput> Regist(RegistMachineInput input)
        {
            input.ValidateUsePassword= Tools.MD5Encrypt32(input.ValidateUsePassword);
            var client = new RestClient($"http://{input.Host}:{input.Port}/api/control-device/regist");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(input);
            request.AddParameter("application/json", body,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ResultBase<RegistResultOutput>>(response.Content);
        }
    }
}