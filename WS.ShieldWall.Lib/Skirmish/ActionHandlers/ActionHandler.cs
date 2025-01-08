namespace WS.ShieldWall.Lib.Skirmish.ActionHandlers;

public interface IActionHander
{
    Type SupportedActionType { get; }
    GameState HandleAction(GameState game, object action);
}

public interface IActionHander<Action> : IActionHander
{
    Type IActionHander.SupportedActionType => typeof(Action);

    GameState IActionHander.HandleAction(GameState game, object action)
    {
        var actionType = action.GetType();
        if (actionType != SupportedActionType)
        {
            throw new ArgumentException($"Action type {actionType} is not supported", nameof(action));
        }
        return HandleAction(game, (Action)action);
    }

    GameState HandleAction(GameState game, Action action);
}

public class ActionHandler
{
    private readonly ImmutableDictionary<Type, IActionHander> actionHandlers;

    public ActionHandler(params IActionHander[] actionHandlers)
    {
        this.actionHandlers = actionHandlers.ToImmutableDictionary(a => a.SupportedActionType);
    }

    public GameState HandleAction(GameState gameState, object action)
    {
        var actionType = action.GetType();
        if (!actionHandlers.TryGetValue(actionType, out IActionHander? actionHander))
        {
            throw new ArgumentException($"Action type {actionType} is not supported", nameof(action));
        }
        return actionHander.HandleAction(gameState, action);
    }
}
