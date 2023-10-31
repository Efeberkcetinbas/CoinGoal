using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPoint : Obstacleable
{
    [SerializeField] private float jumpForce;
    internal override void DoAction(Player player)
    {
        player.SetTempRigidbody();
        player.tempRigidbody.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
    }
}
