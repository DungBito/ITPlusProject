public interface IBombAction {
    void Push(float direction, float force);
    void Blow();
    void Kick(float direction);
    void Pick();
    void Throw();
}
