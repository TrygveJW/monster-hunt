﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private int coinValue = 1;
    public int CoinValue {
        get => coinValue;
        set => coinValue = value;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var args = new CoinCollectedArgs();
            args.Amount = this.coinValue;
            CollectableEvents.OnCoinCollected?.Invoke(this, args);
        }
    }
}