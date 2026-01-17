namespace LX.Commons
{
    public interface ISerializer
    {
        //byte[] Serialize<T>(T message);
        //T Deserialize<T>(byte[] bytes);
        string Serialize<T>(T message);
        T Deserialize<T>(string bytes);

    }
}
