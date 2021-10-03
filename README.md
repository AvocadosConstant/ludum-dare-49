# Untitled Game
#### [Ludum Dare 49](https://ldjam.com/events/ludum-dare/49)

|  |  |
|--|--|
| Theme | Unstable |
| Genre | Top-down shooter |

## Brainstorming

- Player
	- some amorphous matter that is *unstable* and transforms between multiple states
    - state ideas:
        - liquid
            - some slime/goo type state
            - lob slow moving globs over terrain
            - globs affect the area where they land
            - possibilities
                - slows in area
                - marks hit units, marked unit ideas:
                    - slowed
                    - take more damage
                    - combos with other state effects
                    - damage over time (slow)
                - light up an area/ give vision (idea is fluorescent goo)
            - medium speed to move around in this state
        - solid
            - crystalline state (think diamondhead from ben 10)
            - fires fast moving crystal shards
            - shard ideas:
                - high single target damage
                - piercing
                - does more damage to goo'd targets
                - explodes when hitting goo'd targets
            - other attack: close range aoe
                - spikes (think needle kirby)
                - swings crystal shard in an arc
            - slowest to move around in this state
            - takes more damage (brittle)
        - gas
            - push units away?
            - aoe damage over time? (pudge rot from dota)
            - fastest to move around (plus dash? + dash to phase through terrain?)
            - spread gas clouds
	            - shards that travel through gas have additional effects
	            - clouds obstruct enemy vision
	            - clouds damage enemies inside
	            - clouds dissipate over time
        - plasma
	        - something about ionization lol
	        - lightning? neon stuff?
	        - lighting stuff up / providing vision
	- "resource"
	    - when some "resource" is high, you can change states often
        - when it's low, you are stuck in a state until you get more of the "resource"
		- how is it replenished?
		- how is it depleted?
- setting
	- story
	- world
	- NPCs

## Development
- Code
	- Unity
	- should be the easy part 8)
- Art
	- graphics
		- character design
		- animations
		- terrain/backgrounds
	- sound
		- music
		- sound effects
- Story
