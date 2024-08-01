namespace CaMan.Domain.Shared;

public enum ElectoralBodyTypes
{
    AdministrativeRegion, // perifereia
    RegionalUnit, // periferiaki enothta
    Municipality, // dhmos
    MunicipalUnit, // dhmotikh enothta
    
}

public abstract record ElectoralBody(string ShortName, string FullName, string LocalShortName, string LocalFullName);

public record AdministrativeRegion(string ShortName, string LocalShortName, string LocalFullName) : ElectoralBody(ShortName, $"Region of {ShortName}", LocalShortName, LocalFullName);

public record RegionalUnit(string ShortName, string LocalShortName, string LocalFullName) : ElectoralBody(ShortName, $"Regional Unit of {ShortName}", LocalShortName, LocalFullName);

public record Municipality(string ShortName, string FullName, string LocalShortName, string LocalFullName) : ElectoralBody(ShortName, FullName, LocalShortName, LocalFullName);

public record MunicipalUnit(string ShortName, string FullName, string LocalShortName, string LocalFullName) : ElectoralBody(ShortName, FullName, LocalShortName, LocalFullName);

// public static class ElectoralBodies
// {
//     public static List<AdministrativeRegion> AdministrativeRegions =
//     [
//         new AdministrativeRegion("Attica", "Αττική", "Περιφέρεια Αττικής"),
//         new AdministrativeRegion("Central Greece", "Κεντρική Ελλάδα", "Περιφέρεια Κεντρικής Ελλάδας"),
//         new AdministrativeRegion("Central Macedonia", "Κεντρική Μακεδονία", "Περιφέρεια Κεντρικής Μακεδονίας"),
//         new AdministrativeRegion("Region of Crete", "Κρήτη", "Περιφέρεια Κρήτης"),
//         new AdministrativeRegion("Eastern Macedonia and Thrace", "Ανατολική Μακεδονία και Θράκη", "Περιφέρεια Ανατολικής Μακεδονίας και Θράκης"),
//         new AdministrativeRegion("Epirus", "Ήπειρος", "Περιφέρεια Ηπείρου"),
//         new AdministrativeRegion("Ionian Islands", "Ιόνιοι Νήσοι", "Περιφέρεια Ιονίων Νήων"),
//         new AdministrativeRegion("North Aegean", "Βόρειο Αιγαίο", "Περιφέρεια Βορείου Αιγαίου"),
//         new AdministrativeRegion("Peloponnese", "Πελοπόννησος", "Περιφέρεια Πελοποννήσου"),
//         new AdministrativeRegion("Southern Aegean", "Νότιο Αιγαίο", "Περιφέρεια Νοτίου Αιγαίου"),
//         new AdministrativeRegion("Thessaly", "Θεσσαλία", "Περιφέρεια Θεσσαλίας"),
//         new AdministrativeRegion("Western Greece", "Δυτική Ελλάδα", "Περιφέρεια Δυτικής Ελλάδας"),
//         new AdministrativeRegion("Western Macedonia", "Δυτική Μακεδονία", "Περιφέρεια ΔΥτικής Μακεδονίας"),
//     ];
//     public static List<RegionalUnit> RegionalUnits =
//     [
//     ];
//     public static List<Municipality> Municipalities =
//     [
//     ];
//     public static List<MunicipalUnit> MunicipalUnits =
//     [
//     ];
// }