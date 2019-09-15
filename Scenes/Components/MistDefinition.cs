﻿using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings.Scenes.Components {
	public partial class MistDefinition {
		public static MistDefinition AttemptCreate( IEnumerable<MistDefinition> existingMists,
				Rectangle worldArea,
				float densitySquared,
				int aboveGroundMinimumHeight,
				int aboveGroundMaximumHeight,
				TilePattern ground,
				float animationDurationMultiplier ) {
			int x = Main.rand.Next( worldArea.X, worldArea.X + worldArea.Width );
			int y = Main.rand.Next( worldArea.Y, worldArea.Y + worldArea.Height );
			var worldPos = new Vector2( x, y );

			if( ground.Check(x>>4, y>>4) ) {
				return null;
			}

			Vector2 groundPos;
			if( !WorldHelpers.DropToGround( worldPos, false, ground, (y >> 4) + 42, out groundPos ) ) {
				return null;
			}

			groundPos.Y -= aboveGroundMinimumHeight;
			groundPos.Y -= (int)( Main.rand.NextFloat() * (aboveGroundMaximumHeight - aboveGroundMinimumHeight) );

			if( !worldArea.Contains( (int)groundPos.X, (int)groundPos.Y ) ) {
				return null;
			}

			foreach( MistDefinition existingMistDef in existingMists ) {
				// Avoid other mists
				if( Vector2.DistanceSquared( groundPos, existingMistDef.WorldPosition) < densitySquared ) {
					return null;
				}
			}

			Vector2 drift = MistDefinition.GetWindDrift();

			var mistDef = new MistDefinition( groundPos, drift, animationDurationMultiplier );
			mistDef.WorldPosition = groundPos;

			return mistDef;
		}



		////////////////

		public Texture2D CloudTex;
		public Vector2 WorldPosition;
		public Vector2 Velocity;

		public float AnimationDurationMultiplier;
		public float AnimationPercent = 0f;

		public Vector2 Scale = Vector2.One;


		////////////////

		public bool IsActive { get; private set; } = true;



		////////////////

		public MistDefinition( Vector2 worldPos, Vector2 windVelocity, float animationDurationMultiplier=1f ) {
			this.CloudTex = MistDefinition.GetRandomCloudTexture();
			this.WorldPosition = worldPos;
			this.Velocity = windVelocity;
			this.AnimationDurationMultiplier = animationDurationMultiplier;
		}


		////////////////

		public void Update() {
			if( !this.IsActive ) { return; }

			this.WorldPosition += this.Velocity;

			if( this.IsActive ) {
				this.AnimationPercent += ( 1f / 60f ) / this.AnimationDurationMultiplier;
			}

			this.IsActive = this.AnimationPercent < 1f;
		}


		////////////////

		public void Draw( SpriteBatch sb, Color color ) {
			if( !this.IsActive ) { return; }

			var mymod = SurroundingsMod.Instance;
			Vector2 pos = this.WorldPosition - Main.screenPosition;

			float dim = 1f - (Math.Abs( 0.5f - this.AnimationPercent ) * 2f);

			color *= dim;

			if( this.CloudTex != null ) {
				sb.Draw( this.CloudTex, pos, null, color, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f );
			}

			if( mymod.Config.DebugModeInfo ) {
				int wid = (int)((float)this.CloudTex.Width * this.Scale.X);
				int hei = (int)((float)this.CloudTex.Height * this.Scale.Y);

				HUDHelpers.DrawBorderedRect(
					sb,
					Color.Transparent,
					Color.White * 0.25f,
					new Rectangle( (int)pos.X, (int)pos.Y, wid, hei ),
					2
				);
			}
		}
	}
}
