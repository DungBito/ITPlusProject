using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio {
	public class AudioManager : MonoBehaviour {

		public Sound[] sfxs;
		public Sound music;

		public bool muteMusic;
		public bool muteSFX;
		
		public static AudioManager Instance { get; set; }
       
		void Awake () {
			Instance = this;
 
			foreach (Sound s in sfxs) {
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;
 
				s.source.loop = s.loop;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.bypassListenerEffects = s.bypass;
			}

			music.source = gameObject.AddComponent<AudioSource>();
			music.source.clip = music.clip;

			music.source.loop = music.loop;
			music.source.volume = music.volume;
			music.source.pitch = music.pitch;
			music.source.bypassListenerEffects = music.bypass;

			muteSFX = PlayerPrefs.GetInt("SFX", 0) == 1;
			muteMusic = PlayerPrefs.GetInt("Music", 0) == 1;

            if (!muteMusic) {
				PlayMusic();
            }
		}

		public void MuteSFX () {
			muteSFX = !muteSFX;
			PlayerPrefs.SetInt("SFX", muteSFX ? 1 : 0);
			float v = muteSFX ? 0f : 1f;
            foreach (Sound s in sfxs) {
				s.source.volume = v;
            }
		}

		public void MuteMusic () {
			muteMusic = !muteMusic;
			PlayerPrefs.SetInt("Music", muteMusic ? 1 : 0);
            if (muteMusic) {
				StopMusic();
            }
            else {
				PlayMusic();
            }
		}
 
		public void PlaySFX(string n) {
            if (muteSFX) {
				return;
            }
			foreach (Sound s in sfxs) {
				if (s.name == n) {
					s.source.Play();
					return;
				}
			}
		}

		public void StopSFX(string n) {
			foreach (Sound s in sfxs) {
				if (s.name == n) {
					s.source.Stop();
					return;
				}
			}
		}

		public void PlayMusic () {
			music.source.Play();
		}

		public void StopMusic () {
			music.source.Stop();
        }
	}
}

