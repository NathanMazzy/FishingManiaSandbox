using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Sandbox
{
	public partial class FishingManiaPlayer : Player
	{
		[Net] public int score { get; set; }
		public override void Spawn()
		{
			base.Spawn();
			score = 50;
			//
			// Use a watermelon model
			//
			SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );
			CameraMode = new FirstPersonCamera();
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			Rotation = Input.Rotation;
			EyeRotation = Rotation;

			// build movement from the input values
			var movement = new Vector3( Input.Forward, Input.Left, 0 ).Normal;

			// rotate it to the direction we're facing
			Velocity = Rotation * movement;

			// apply some speed to it
			Velocity *= Input.Down( InputButton.Run ) ? 1000 : 200;

			// apply it to our position using MoveHelper, which handles collision
			// detection and sliding across surfaces for us
			MoveHelper helper = new MoveHelper( Position, Velocity );
			helper.Trace = helper.Trace.Size( 16 );
			if ( helper.TryMove( Time.Delta ) > 0 )
			{
				Position = helper.Position;
			}

			// If we're running serverside and Attack1 was just pressed, spawn a ragdoll
			if ( IsServer && Input.Down( InputButton.Attack1 )  && score > 0)
			{
				var ragdoll = new ModelEntity();
				ragdoll.SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );
				ragdoll.Position = EyePosition + EyeRotation.Forward * 40;
				ragdoll.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				ragdoll.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				ragdoll.PhysicsGroup.Velocity = EyeRotation.Forward * 1000;
				score--;
			}

			if ( IsServer && Input.Pressed( InputButton.Reload ) )
			{
				score = 250;
			}
			if ( IsServer && Input.Pressed( InputButton.Grenade ) )
			{
				Log.Info( "Im the server" );
			}
		}

		/// <summary>
		/// Called every frame on the client
		/// </summary>
		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			// Update rotation every frame, to keep things smooth
			Rotation = Input.Rotation;
			EyeRotation = Rotation;
		}

	}
}

