namespace KuyumcuWebApi.exception;


public class ConflictException:Exception {

    public ConflictException(string message) : base(message) {}
}