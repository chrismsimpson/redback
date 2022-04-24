
namespace Redback;

public partial class Holder {

    public String Hash { get; init; }

    public String Name { get; init; }

    ///

    public Holder(
        String hash,
        String name) {

        this.Hash = hash;
        this.Name = name;
    }
}