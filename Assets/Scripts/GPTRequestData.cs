[System.Serializable]
public class GPTRequestData
{
    public string system_prompt;
    public string user_input;
    public string api_key;
    public bool use_16k_model;

    public GPTRequestData(string system_prompt, string user_input, bool use_16k_model, string api_key)
    {
        this.system_prompt = system_prompt;
        this.user_input = user_input;
        this.api_key = api_key;
        this.use_16k_model = use_16k_model;
    }
}
