using Content.Shared.Actions;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Serialization;
using Robust.Shared.Containers;
using Robust.Shared.Audio;
using Robust.Shared.Network;
using Content.Shared.Hands.EntitySystems;
using System.Linq;
using Content.Shared.ADT.SpeechBarks;
using Content.Server.Chat.Systems;
using Robust.Shared.Configuration;
using Content.Shared.ADT.CCVar;
using Content.Server.Mind;
using Robust.Shared.Player;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;

namespace Content.Server.ADT.SpeechBarks;

public sealed class SpeechBarksSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly ISharedPlayerManager _player = default!;
    private bool _isEnabled = false;

    public override void Initialize()
    {
        base.Initialize();

        _cfg.OnValueChanged(ADTCCVars.BarksEnabled, v => _isEnabled = v, true);

        SubscribeLocalEvent<SpeechBarksComponent, EntitySpokeEvent>(OnEntitySpoke);
    }

    private void OnEntitySpoke(EntityUid uid, SpeechBarksComponent component, EntitySpokeEvent args)
    {
        if (!_isEnabled)
            return;

        var ev = new TransformSpeakerBarkEvent(uid, component.Data.Sound?.ToString() ?? string.Empty, component.Data.Pitch);
        RaiseLocalEvent(uid, ev);

        var message = args.Message;
        var soundSpecifier = component.Data.Sound;
        if (soundSpecifier == null && !string.IsNullOrEmpty(ev.Sound) && _proto.TryIndex<BarkPrototype>(ev.Sound, out var barkPrototype))
        {
            soundSpecifier = barkPrototype.Sound;
        }

        foreach (var ent in _lookup.GetEntitiesInRange(Transform(uid).Coordinates, 10f))
        {
            if (!_mind.TryGetMind(ent, out _, out var mind) || mind.UserId == null || !_player.TryGetSessionById(mind.UserId, out var session))
                continue;

            RaiseNetworkEvent(new PlaySpeechBarksEvent(
                        GetNetEntity(uid),
                        message,
                        soundSpecifier ?? new SoundPathSpecifier("/Audio/Voice/Human/male1.ogg"),
                        ev.Pitch,
                        component.Data.MinVar,
                        component.Data.MaxVar,
                        args.IsWhisper), session);
        }
    }
}
