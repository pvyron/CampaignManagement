using CaMan.DomainOld.Shared;

namespace CaMan.DomainOld.ElectoralBodies;

public abstract class ElectoralBody
{
    internal ElectoralBody(ShortName shortName, FullName fullName)
    {
        Id = new(Ulid.NewUlid());
        ShortName = shortName;
        FullName = fullName;
    }

    public ElectoralBodyId Id { get; private set; }
    public ShortName ShortName { get; private set; }
    public FullName FullName { get; private set; }
}

/// <summary>
/// Περιφέρεια
/// </summary>
public sealed class AdministrativeRegion : ElectoralBody
{
    private AdministrativeRegion(ShortName shortName, FullName fullName) : base(shortName, fullName)
    {
    }

    public static AdministrativeRegion Create(ShortName shortName, FullName fullName) => new(shortName, fullName);
}

/// <summary>
/// Περιφερειακή ενότητα
/// </summary>
public sealed class RegionalUnit : ElectoralBody
{
    private RegionalUnit(ShortName shortName, FullName fullName) : base(shortName, fullName)
    {
    }

    public static RegionalUnit Create(ShortName shortName, FullName fullName) => new(shortName, fullName);
}

/// <summary>
/// Δήμος
/// </summary>
public sealed class Municipality : ElectoralBody
{
    private Municipality(ShortName shortName, FullName fullName) : base(shortName, fullName)
    {
    }

    public static Municipality Create(ShortName shortName, FullName fullName) => new(shortName, fullName);
}

/// <summary>
/// Δημοτική ενότητα
/// </summary>
public sealed class MunicipalUnit : ElectoralBody
{
    private MunicipalUnit(ShortName shortName, FullName fullName) : base(shortName, fullName)
    {
    }

    public static MunicipalUnit Create(ShortName shortName, FullName fullName) => new(shortName, fullName);
}