using Content.Shared.ADT.CCVar;
using Content.Shared.VoiceMask;
using Content.Server.ADT.SpeechBarks;
using Content.Shared.ADT.SpeechBarks;
using Robust.Shared.Configuration;
using Content.Shared.Inventory;
using Robust.Shared.Prototypes;

namespace Content.Server.VoiceMask;

public partial class VoiceMaskSystem
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;

    private void InitializeBarks()
    {
        SubscribeLocalEvent<VoiceMaskComponent, Content.Server.ADT.SpeechBarks.TransformSpeakerBarkEvent>(OnSpeakerVoiceTransform);
        SubscribeLocalEvent<VoiceMaskComponent, VoiceMaskChangeBarkMessage>(OnChangeBark);
        SubscribeLocalEvent<VoiceMaskComponent, VoiceMaskChangeBarkPitchMessage>(OnChangePitch);
    }

    private void OnSpeakerVoiceTransform(EntityUid uid, VoiceMaskComponent component, Content.Server.ADT.SpeechBarks.TransformSpeakerBarkEvent args)
    {
        if (!_proto.TryIndex<BarkPrototype>(component.BarkId, out var proto)) // Исправлено
            return;

        args.Sound = proto.ID;
    }

    private void OnChangeBark(EntityUid uid, VoiceMaskComponent component, VoiceMaskChangeBarkMessage message)
    {
        component.BarkId = message.Proto;
        _popupSystem.PopupEntity(Loc.GetString("voice-mask-voice-popup-success"), uid);
        UpdateUI((uid, component));
    }

    private void OnChangePitch(EntityUid uid, VoiceMaskComponent component, VoiceMaskChangeBarkPitchMessage message)
    {
        _popupSystem.PopupEntity(Loc.GetString("voice-mask-voice-popup-success"), uid);
        UpdateUI((uid, component));
    }
}