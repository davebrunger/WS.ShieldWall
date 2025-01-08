namespace WS.HailCaesar.Lib.Models;

public record Unit
(
    string Name,
    UnitSize Size,
    UnitTemplate Template,
    UnitFormation Formation,
    IImmutableSet<UnitState> States,
    int Casualties
)
{
    public int GetRangedDice(int range, int buildingSides = 0)
    {
        var maxDiceForFormation = Formation.GetMaxRangedDice();
        var maxDiceForBuilding = buildingSides == 0 ? int.MaxValue : buildingSides * 2;
        var rangeDice = range <= 6 ? Template.ShortRange : (range <= Template.MaxRange ? Template.LongRange : 0);
        return new int[] {
            maxDiceForFormation,
            maxDiceForBuilding,
            Size switch {
                UnitSize.Tiny => 1,
                UnitSize.Small => rangeDice - 1,
                UnitSize.Large => rangeDice + 1,
                _ => rangeDice
            }
        }.Min();
    }

    public int GetHandToHandDice(bool isClash = false, int squareSides = 0, int buildingSides = 0)
    {
        var maxDiceForFormation = Formation.GetMaxHandToHandDice(squareSides);
        var maxDiceForBuilding = buildingSides == 0 ? int.MaxValue : buildingSides * 2;
        var handToHandDice = isClash ? Template.Clash : Template.Sustained;
        return new int[] {
            maxDiceForFormation,
            maxDiceForBuilding,
            Size switch {
                UnitSize.Tiny => 1,
                UnitSize.Small => handToHandDice - 2,
                UnitSize.Large => handToHandDice + 2,
                _ => handToHandDice
            }
        }.Min();
    }

    public int GetRangedModifier(Unit target, int range, bool isPartlyObscured = false, bool isInTargetsFrontQuarter = false, bool isClosingShot = false, bool isTraversingShot = false)
    {
        return new int[]
            {
                States.Contains(UnitState.Shaken) || States.Contains(UnitState.Disordered) ? -1 : 0,
                isPartlyObscured ||
                    target.Template.SubType.GetUnitType() == UnitType.Artillery ||
                    target.Template.SubType.GetUnitType() == UnitType.Baggage ||
                    !target.Formation.IsFormed() ? -1 : 0,
                target.Formation.IsFormed() &&
                    ((target.Template.SubType == UnitSubType.HeavyInfantry && isInTargetsFrontQuarter) || target.Template.SubType == UnitSubType.Cataphracts) ? -1 : 0,
                isClosingShot ? -1 : 0,
                isTraversingShot ? -1 : 0,
                range > 6 ? -1 : 0
            }.Sum();
    }

    public int GetHandToHandModifier(bool isCharging = false, bool isWinning = false, bool isUphillOfTarget = false, bool isEngagedToFlankOrRear = false, bool hasMovedLastRound = false)
    {
        return new int[]
            {
                isCharging ? +1 : 0,
                isWinning ? +1 : 0,
                isUphillOfTarget && !(isCharging || hasMovedLastRound) ? +1 : 0,
                States.Contains(UnitState.Shaken) || States.Contains(UnitState.Disordered) ? -1 : 0,
                !Formation.IsFormed() ? -1 : 0,
                isEngagedToFlankOrRear ? -1 : 0,
            }.Sum();
    }

    public int GetRangedMoraleSaveModifier(Unit attacker, bool isInCover = false, bool isInBuilding = false, bool isInFortification = false)
    {
        return new int[]
            {
                Formation == UnitFormation.Square || Formation == UnitFormation.Wedge ? +1 : 0,
                isInCover ? +1 : 0,
                Formation == UnitFormation.Testudo ? +2 : 0,
                isInBuilding ? +2 : 0,
                isInFortification ? +3 : 0,
                attacker.Template.SubType == UnitSubType.LightArtillery ? -1 : 0,
                attacker.Template.SubType == UnitSubType.MediumArtillery || attacker.Template.SubType == UnitSubType.MediumArtillery ? -2 : 0,
                Formation == UnitFormation.Column ? -1 : 0,
            }.Sum();
    }

    public IImmutableSet<RangedBreakTestResult> GetRangedBreakTestResult(int testResult)
    {
        return (Template.SubType.GetUnitCategory(), testResult) switch
        {
            (_, _) when testResult >= 10 => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),

            (UnitCategory.Infantry, 9) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),
            (UnitCategory.Cavalry, 9) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),
            (UnitCategory.Skirmishers, 9) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatInGoodOrder),

            (UnitCategory.Infantry, 8) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),
            (UnitCategory.Cavalry, 8) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),
            (UnitCategory.Skirmishers, 8) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),

            (UnitCategory.Infantry, 7) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldInGoodOrder),
            (UnitCategory.Cavalry, 7) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatInGoodOrder),
            (UnitCategory.Skirmishers, 7) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),

            (UnitCategory.Infantry, 6) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldDisordered).Add(RangedBreakTestResult.RetreatInGoodOrder),
            (UnitCategory.Cavalry, 6) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Skirmishers, 6) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),

            (UnitCategory.Infantry, 5) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.HoldDisordered).Add(RangedBreakTestResult.RetreatInGoodOrder),
            (UnitCategory.Cavalry, 5) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Skirmishers, 5) when States.Contains(UnitState.Shaken) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Skirmishers, 5) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),

            (UnitCategory.Infantry, 4) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Cavalry, 4) when States.Contains(UnitState.Shaken) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Cavalry, 4) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Skirmishers, 4) when Casualties > 0 => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Skirmishers, 4) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatInGoodOrder),

            (UnitCategory.Infantry, 3) when States.Contains(UnitState.Shaken) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Infantry, 3) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Cavalry, 3) when States.Contains(UnitState.Shaken) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Cavalry, 3) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Skirmishers, _) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),

            (UnitCategory.Infantry, _) when Casualties > 0 => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Infantry, _) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),
            (UnitCategory.Cavalry, _) when Casualties > 0 => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.Break),
            (UnitCategory.Cavalry, _) => ImmutableHashSet<RangedBreakTestResult>.Empty.Add(RangedBreakTestResult.RetreatDisordered),

            (_, _) => throw new IndexOutOfRangeException(nameof(UnitCategory)),
        };
    }
    public (HandToHandBreakTestResult UnitResult, HandToHandBreakTestResult SupportersResult) GetHandToHandBreakTestResult(int testResult)
    {
        return (Template.SubType.GetUnitCategory(), testResult) switch
        {
            (_, _) when testResult >= 10 => (HandToHandBreakTestResult.Hold, HandToHandBreakTestResult.Hold),

            (UnitCategory.Infantry, 9) => (HandToHandBreakTestResult.Hold, HandToHandBreakTestResult.Hold),
            (UnitCategory.Cavalry, 9) => (HandToHandBreakTestResult.GiveGroundInGoodOrder, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Skirmishers, 9) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.Hold),

            (UnitCategory.Infantry, 8) => (HandToHandBreakTestResult.Hold, HandToHandBreakTestResult.Hold),
            (UnitCategory.Cavalry, 8) => (HandToHandBreakTestResult.GiveGroundInGoodOrder, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Skirmishers, 8) when States.Contains(UnitState.Shaken) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
            (UnitCategory.Skirmishers, 8) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.Hold),

            (UnitCategory.Infantry, 7) => (HandToHandBreakTestResult.GiveGroundInGoodOrder, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Cavalry, 7) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Skirmishers, _) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),

            (UnitCategory.Infantry, 6) => (HandToHandBreakTestResult.GiveGroundInGoodOrder, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Cavalry, 6) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundInGoodOrder),

            (UnitCategory.Infantry, 5) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundInGoodOrder),
            (UnitCategory.Cavalry, 5) when States.Contains(UnitState.Shaken) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
            (UnitCategory.Cavalry, 5) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundDisordered),

            (UnitCategory.Infantry, 4) when States.Contains(UnitState.Shaken) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
            (UnitCategory.Infantry, 4) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundDisordered),
            (UnitCategory.Cavalry, 4) when States.Contains(UnitState.Shaken) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
            (UnitCategory.Cavalry, 4) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundDisordered),

            (UnitCategory.Infantry, 3) when States.Contains(UnitState.Shaken) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
            (UnitCategory.Infantry, 3) => (HandToHandBreakTestResult.GiveGroundDisordered, HandToHandBreakTestResult.GiveGroundDisordered),
            (UnitCategory.Cavalry, 3) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),

            (_, _) => (HandToHandBreakTestResult.Break, HandToHandBreakTestResult.Hold),
        };
    }
}
