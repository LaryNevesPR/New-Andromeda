// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 CerberusWolfie <wb.johnb.willis@gmail.com>
// SPDX-FileCopyrightText: 2025 Eris <eris@erisws.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 SX-7 <sn1.test.preria.2002@gmail.com>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Power;
using Robust.Client.GameObjects;

namespace Content.Client.PowerCell;

public sealed class PowerChargerVisualizerSystem : VisualizerSystem<PowerChargerVisualsComponent>
{
    protected override void OnAppearanceChange(EntityUid uid, PowerChargerVisualsComponent comp, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        // Update base item
        if (AppearanceSystem.TryGetData<bool>(uid, CellVisual.Occupied, out var occupied, args.Component) && occupied)
        {
            // TODO: don't throw if it doesn't have a full state
            args.Sprite.LayerSetState(PowerChargerVisualLayers.Base, comp.OccupiedState);
        }
        else
        {
            args.Sprite.LayerSetState(PowerChargerVisualLayers.Base, comp.EmptyState);
        }

        // Update lighting
        if (AppearanceSystem.TryGetData<CellChargerStatus>(uid, CellVisual.Light, out var status, args.Component)
        &&  comp.LightStates.TryGetValue(status, out var lightState))
        {
            args.Sprite.LayerSetState(PowerChargerVisualLayers.Light, lightState);
            args.Sprite.LayerSetVisible(PowerChargerVisualLayers.Light, true);
        }
        else
            //
            args.Sprite.LayerSetVisible(PowerChargerVisualLayers.Light, false);
    }
}

enum PowerChargerVisualLayers : byte
{
    Base,
    Light,
    ItemDisplay, // WWDP
}
