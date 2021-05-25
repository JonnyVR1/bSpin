﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bSpin.Extentions
{
	public static class TransformExtensions
	{
		//similar to the one found in the LivFinder, but should work for ALL angles, not just behind you
		public static float GetRotation(this Transform target, Vector3 origin)
        {
			return GetRot(target, origin);
        }
		public static float GetRotation(this Transform target)
		{
			return GetRot(target, Vector3.zero);
		}
		internal static float GetRot(Transform target, Vector3 origin)
        {
			//since we have to deal with more than just 180° of bullshit, we're gonna have to do some more math than usual

			//pointing to the left returns negative values, and right returns positive, hopefully up to 180° in either direction

			//and since unity is weird, Z is forward
			//gonna draw a triangle here real quick
			///
			///         |\
			///         | \
			///adjacent |  \
			///         |   \
			///         |____\
			///         opposite
			/// 
			/// geometry class coming in clutch
			
			float opposite = target.position.x;
			float adjacent = target.position.z;

			bool left = opposite < 0;
			bool right = opposite > 0;
			bool back = adjacent < 0;

			opposite = Math.Abs(opposite);
			adjacent = Math.Abs(adjacent);

			float angle = (float)(Math.Atan(opposite / adjacent) * (180 / Math.PI));

			//since we forced positive earlier, this offsets it to be where we pointed
			if (back && left)
				angle -= 90;
			if (back && right)
				angle += 90;
			if (left && !back)
				angle = -angle;
			return angle;
		}

    }

    public static class MovementExtentions
    {
		public static IEnumerator ExperimentalSpin(this Transform transform, CustomTypes.Spin speen, float speed, float offset = 0.0f)
        {
			yield return new WaitForSecondsRealtime(speen.DelayBeforeSpin / speed);
			float i = 0.0f;
			float sT = Time.time;
			Vector3 lastAng = speen.Begin;
			while (i < 1.0f)
			{
				i = (Time.time - sT) / (speen.Length / speed);

				EasingFunction.Ease ease = speen.Easing;
				EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

				float x = func(speen.Begin.x, speen.End.x, i);
				float nx = x - lastAng.x;
				float y = func(speen.Begin.y + offset, speen.End.y + offset, i);
				float ny = y - lastAng.y;
				float z = func(speen.Begin.z, speen.End.z, i);
				float nz = z - lastAng.z;
				transform.localEulerAngles += new Vector3(nx, ny, nz);
				lastAng = new Vector3(x, y, z);
				yield return null;
			}
			Plugin.Log.Debug("Spin finished in " + Mathf.RoundToInt(Time.time - sT) + " Seconds");
			yield return new WaitForSecondsRealtime(speen.DelayAfterSpin / speed);
		}
		public static IEnumerator ExperimentalNoodleSpin(this Transform transform, CustomTypes.Spin speen, float speed, float offset = 0.0f)
		{
			float pT = Time.time;
			float a = 0.0f;
			while (a < 1.0f)
			{
				//noodle's a piece of shit isn't it?
				a = (Time.time - pT) / (speen.DelayBeforeSpin / speed);
				transform.localEulerAngles = speen.Begin;
				yield return null;
			}
			float i = 0.0f;
			float sT = Time.time;
			Vector3 lastAng = speen.Begin;
			while (i < 1.0f)
			{
				i = (Time.time - sT) / (speen.Length / speed);

				EasingFunction.Ease ease = speen.Easing;
				EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

				float x = func(speen.Begin.x, speen.End.x, i);
				float nx = x - lastAng.x;
				float y = func(speen.Begin.y + offset, speen.End.y + offset, i);
				float ny = y - lastAng.y;
				float z = func(speen.Begin.z, speen.End.z, i);
				float nz = z - lastAng.z;
				transform.localEulerAngles += new Vector3(nx, ny, nz);
				lastAng = new Vector3(x, y, z);
				yield return null;
			}
			Plugin.Log.Debug("Spin finished in " + Mathf.RoundToInt(Time.time - sT) + " Seconds");
			float aT = Time.time;
			float b = 0.0f;
			while (b < 1.0f)
			{
				b = (Time.time - aT) / (speen.DelayBeforeSpin / speed);
				transform.localEulerAngles = speen.End;
				yield return null;
			}
		}
		public static IEnumerator Spin(this Transform transform, CustomTypes.Spin speen, float speed, float offset = 0.0f)
        {
			yield return new WaitForSecondsRealtime(speen.DelayBeforeSpin / speed);
			float i = 0.0f;
			float sT = Time.time;
			while (i < 1.0f)
			{
				i = (Time.time - sT) / (speen.Length / speed);

				EasingFunction.Ease ease = speen.Easing;
				EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

				float x = func(speen.Begin.x, speen.End.x, i);
				float y = func(speen.Begin.y + offset, speen.End.y + offset, i);
				float z = func(speen.Begin.z, speen.End.z, i);

				transform.localEulerAngles = new Vector3(x, y, z);
				yield return null;
			}
			Plugin.Log.Debug("Spin finished in " + Mathf.RoundToInt(Time.time - sT) + " Seconds");
			yield return new WaitForSecondsRealtime(speen.DelayAfterSpin / speed);
		}
        public static IEnumerator NoodleSpin(this Transform transform, CustomTypes.Spin speen, float speed, float offset = 0.0f)
        {
			float pT = Time.time;
			float a = 0.0f;
			while (a < 1.0f)
			{
				//noodle's a piece of shit isn't it?
				a = (Time.time - pT) / (speen.DelayBeforeSpin / speed);
				transform.localEulerAngles = speen.Begin;
				yield return null;
			}
			float i = 0.0f;
			float sT = Time.time;
			while (i < 1.0f)
			{
				i = (Time.time - sT) / (speen.Length / speed);

				EasingFunction.Ease ease = speen.Easing;
				EasingFunction.Function func = EasingFunction.GetEasingFunction(ease);

				float x = func(speen.Begin.x, speen.End.x, i);
				float y = func(speen.Begin.y + offset, speen.End.y + offset, i);
				float z = func(speen.Begin.z, speen.End.z, i);

				transform.localEulerAngles = new Vector3(x, y, z);
				yield return null;
			}
			Plugin.Log.Debug("Spin finished in " + Mathf.RoundToInt(Time.time - sT) + " Seconds");
			float aT = Time.time;
			float b = 0.0f;
			while (b < 1.0f)
			{
				b = (Time.time - aT) / (speen.DelayBeforeSpin / speed);
				transform.localEulerAngles = speen.End;
				yield return null;
			}
		}
    }
}
