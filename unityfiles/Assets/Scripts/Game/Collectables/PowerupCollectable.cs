﻿using System;

/// <summary>
/// Arguments for powerup collected event
/// </summary>
public class PowerUpCollectedArgs : EventArgs {
    public PICKUP_TYPE Effect { get; set; }
}

public class PowerupCollectable : Collectable {
    public static event EventHandler<PowerUpCollectedArgs> OnPowerupCollected;

    private MoveToGuiElement moveToGuiElement;

    private readonly string name = "Power up";

    private PICKUP_TYPE effectPickup;

    public PICKUP_TYPE EffectPickup {
        get => effectPickup;
        set => effectPickup = value;
    }

    private void Awake() {
        if (TryGetComponent(out moveToGuiElement)) {
            moveToGuiElement.FindTarget<LettersCollectedGUI>();
        }
    }

    private void OnDestroy() {
        PowerUpCollectedArgs powerupArgs = new PowerUpCollectedArgs();
        powerupArgs.Effect = effectPickup;
        PowerupCollectable.OnPowerupCollected?.Invoke(this, powerupArgs);
    }

    public override string Name {
        get => name;
    }
}